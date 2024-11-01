using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Abstractions.Enums;
using NetModular.Lib.Data.Abstractions.Options;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Utils.Core.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace NetModular.Lib.Data.Integration
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加数据库
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cfg"></param>
        /// <param name="modules"></param>
        public static void AddDb(this IServiceCollection services, IConfiguration cfg, IModuleCollection modules)
        {
            if (modules == null || !modules.Any())
                return;

            var dbOptions = new DbOptions();
            cfg.GetSection("Db").Bind(dbOptions);

            if (dbOptions?.Modules == null || !dbOptions.Modules.Any())
                return;

            CheckOptions(dbOptions);

            services.AddSingleton(dbOptions);

            services.Configure<DbOptions>(m =>
            {
                foreach (var propertyInfo in typeof(DbOptions).GetProperties())
                {
                    propertyInfo.SetValue(m, propertyInfo.GetValue(dbOptions));
                }
            });

            foreach (var options in dbOptions.Modules)
            {
                var module = modules.FirstOrDefault(m => m.Code.EqualsIgnoreCase(options.Name));
                if (module != null)
                {
                    services.AddDbContext(module, options, dbOptions);

                    services.AddEntityObserver(module);
                }
            }
        }

        private static void CheckOptions(DbOptions options)
        {
            foreach (var dbModuleOptions in options.Modules)
            {
                Check.NotNull(dbModuleOptions.Name, "DbModuleOptions.Name", "数据库配置项名称不能为空");

                if (dbModuleOptions.Database.IsNull())
                {
                    dbModuleOptions.Database = dbModuleOptions.Name;
                }

                //MySql数据库和PostgreSQL模式名称转小写
                if (options.Dialect == SqlDialect.MySql || options.Dialect == SqlDialect.PostgreSQL)
                {
                    dbModuleOptions.Database = dbModuleOptions.Database.ToLower();
                }
            }

            //PostgreSQL数据库名称转小写
            if (options.NpgsqlDatabaseName.NotNull() && options.Dialect == SqlDialect.PostgreSQL)
            {
                options.NpgsqlDatabaseName = options.NpgsqlDatabaseName.ToLower();
            }
        }

        /// <summary>
        /// 添加数据库上下文
        /// </summary>
        private static void AddDbContext(this IServiceCollection services, IModuleDescriptor module, DbModuleOptions options, DbOptions dbOptions)
        {
            var dbContextType = module.AssemblyDescriptor.Infrastructure.GetTypes().FirstOrDefault(m => m.Name.EqualsIgnoreCase(options.Name + "DbContext"));
            if (dbContextType != null)
            {
                //加载实体列表
                LoadEntityTypes(module, options);

                var dbContextOptionsAssemblyName = AssemblyHelper.GetCurrentAssemblyName().Replace("Integration", "") + dbOptions.Dialect;
                var dbContextOptionsTypeName = dbOptions.Dialect + "DbContextOptions";

                var dbContextOptionsType = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(dbContextOptionsAssemblyName)).GetType($"{dbContextOptionsAssemblyName}.{dbContextOptionsTypeName}");

                var sp = services.BuildServiceProvider();

                //日志工厂
                var loggerFactory = dbOptions.Logging ? sp.GetService<ILoggerFactory>() : null;
                //登录信息
                var loginInfo = sp.GetService<ILoginInfo>();

                //数据库上下文配置项
                var contextOptions = (IDbContextOptions)Activator.CreateInstance(dbContextOptionsType, dbOptions, options, loggerFactory, loginInfo);

                //数据库创建事件
                var createDatabaseEvent = module.AssemblyDescriptor.Infrastructure.GetTypes().FirstOrDefault(m => typeof(IDatabaseCreateEvents).IsAssignableFrom(m));
                if (createDatabaseEvent != null)
                {
                    contextOptions.DatabaseCreateEvents = (IDatabaseCreateEvents)Activator.CreateInstance(createDatabaseEvent);
                }

                //创建数据库上下文实例
                var dbContext = (IDbContext)Activator.CreateInstance(dbContextType, contextOptions);

                #region ==执行初始化脚本==

                //当开启初始化脚本 && 开启自动创建数据库 && 数据库不存在
                if (dbOptions.InitData && dbOptions.CreateDatabase && !dbContext.DatabaseExists)
                {
                    var dbScriptPath = "";
                    switch (dbOptions.Dialect)
                    {
                        case SqlDialect.SqlServer:
                            dbScriptPath = module.InitDataScriptDescriptor.SqlServer;
                            break;
                        case SqlDialect.MySql:
                            dbScriptPath = module.InitDataScriptDescriptor.MySql;
                            break;
                        case SqlDialect.SQLite:
                            dbScriptPath = module.InitDataScriptDescriptor.SQLite;
                            break;
                        case SqlDialect.PostgreSQL:
                            dbScriptPath = module.InitDataScriptDescriptor.PostgreSQL;
                            break;
                        case SqlDialect.Oracle:
                            dbScriptPath = module.InitDataScriptDescriptor.Oracle;
                            break;
                    }

                    if (dbScriptPath.NotNull() && File.Exists(dbScriptPath))
                    {
                        using var sr = new StreamReader(dbScriptPath);
                        var sql = sr.ReadToEnd();

                        if (sql.NotNull())
                        {
                            if (dbOptions.Dialect == SqlDialect.PostgreSQL)
                            {
                                sql = sql.Replace("[nm_database_name]", options.Database);
                            }

                            //此处不能使用IDbContext的NewConnection方法创建连接
                            var con = dbContext.Options.NewConnection();

                            con.Execute(sql);
                        }
                    }

                    var jsonDir = module.InitDataScriptDescriptor.JsonDataDirectory;
                    if (jsonDir.NotNull() && Directory.Exists(jsonDir))
                    {
                        string[] jsonFiles = Directory.GetFiles(jsonDir, "*.json");

                        var genericType = typeof(DbSet<>);
                        foreach (var jsonFile in jsonFiles)
                        {
                            string typeName = Path.GetFileNameWithoutExtension(jsonFile);
                            var entityType = dbContext.Options.DbModuleOptions.EntityTypes.Find(a => string.Equals(a.Name, typeName, StringComparison.OrdinalIgnoreCase) || a.Name.EndsWith("Entity") && string.Equals(a.Name.Substring(0, a.Name.Length - 6), typeName, StringComparison.OrdinalIgnoreCase));

                            if (entityType == null) continue;

                            using var sr = new StreamReader(jsonFile);
                            var json = sr.ReadToEnd();
                            var list = JsonConvert.DeserializeObject(json, typeof(List<>).MakeGenericType(entityType));

                            var dbSetType = genericType.MakeGenericType(entityType);
                            var db = Activator.CreateInstance(dbSetType, new object[] { dbContext });
                            dbSetType.GetMethod("BatchInsert")!.Invoke(db, new object[] { list, 10000, null, null });
                        }
                    }
                }

                #endregion

                //模块描述设置数据库上下文
                module.DbContext = dbContext;

                //注入数据库上下文
                services.AddSingleton(dbContextType, dbContext);

                services.AddRepositories(module, dbContext, dbOptions);
            }
        }

        /// <summary>
        /// 添加仓储
        /// </summary>
        private static void AddRepositories(this IServiceCollection services, IModuleDescriptor module, IDbContext dbContext, DbOptions dbOptions)
        {
            var interfaceList = module.AssemblyDescriptor.Domain.GetTypes().Where(t => t.IsInterface && typeof(IRepository<>).IsImplementType(t)).ToList();

            if (!interfaceList.Any())
                return;

            //根据仓储的命名空间名称来注入不同数据库的仓储
            var entityNamespacePrefix = $"{module.AssemblyDescriptor.Infrastructure.GetName().Name}.Repositories.{dbOptions.Dialect}.";
            foreach (var serviceType in interfaceList)
            {
                var implementType = module.AssemblyDescriptor.Infrastructure.GetTypes().FirstOrDefault(m => m.FullName.NotNull() && m.FullName.StartsWith(entityNamespacePrefix) && serviceType.IsAssignableFrom(m));
                if (implementType != null)
                {
                    services.AddSingleton(serviceType, Activator.CreateInstance(implementType, dbContext));
                }
            }
        }

        /// <summary>
        /// 加载实体类型列表
        /// </summary>
        /// <param name="module"></param>
        /// <param name="options"></param>
        private static void LoadEntityTypes(IModuleDescriptor module, DbModuleOptions options)
        {
            options.EntityTypes = module.AssemblyDescriptor.Domain.GetTypes().Where(t => t.IsClass && typeof(IEntity).IsImplementType(t)).ToList();
        }

        /// <summary>
        /// 注入实体观察者
        /// </summary>
        /// <param name="services"></param>
        /// <param name="module"></param>
        private static void AddEntityObserver(this IServiceCollection services, IModuleDescriptor module)
        {
            var observers = module.AssemblyDescriptor.Application.GetTypes().Where(t => typeof(IEntityObserver<>).IsImplementType(t)).ToList();
            observers.AddRange(module.AssemblyDescriptor.Infrastructure.GetTypes().Where(t => typeof(IEntityObserver<>).IsImplementType(t)).ToList());
            observers.AddRange(module.AssemblyDescriptor.Domain.GetTypes().Where(t => typeof(IEntityObserver<>).IsImplementType(t)).ToList());
            observers.ForEach(m =>
            {
                var interfaceType = m.GetInterfaces().FirstOrDefault();
                if (interfaceType != null)
                {
                    services.AddSingleton(interfaceType, m);
                }
            });

            services.AddSingleton<IEntityObserverHandler, EntityObserverHandler>();
        }

        /// <summary>
        /// 注入实体观察者处理器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="modules"></param>
        public static void AddEntityObserversHandler(this IServiceCollection services, IModuleCollection modules)
        {
            var sp = services.BuildServiceProvider();
            var dbOptions = sp.GetService<DbOptions>();
            if (dbOptions.Monitoring)
            {
                var observerHandler = new EntityObserverHandler(sp);
                foreach (var options in dbOptions.Modules)
                {
                    var dbContext = modules.FirstOrDefault(m => m.Code.EqualsIgnoreCase(options.Name))?.DbContext;
                    if (dbContext != null)
                    {
                        ((DbContext)dbContext).ObserverHandler = observerHandler;
                    }
                }
                services.AddSingleton<IEntityObserverHandler>(observerHandler);
            }
        }
    }
}
