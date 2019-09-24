using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Entities;
using Nm.Lib.Data.Abstractions.Options;
using Nm.Lib.Module.Abstractions;
using Nm.Lib.Utils.Core;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Utils.Core.Helpers;

namespace Nm.Lib.Data.Integration
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加数据库
        /// </summary>
        /// <param name="services"></param>
        /// <param name="environmentName"></param>
        /// <param name="modules"></param>
        public static void AddDb(this IServiceCollection services, string environmentName, IModuleCollection modules)
        {
            if (modules == null || !modules.Any())
                return;

            var cfgHelper = new ConfigurationHelper();
            var dbOptions = cfgHelper.Get<DbOptions>("Db", environmentName);

            if (dbOptions?.Modules == null || !dbOptions.Modules.Any())
                return;

            CheckOptions(dbOptions);

            services.AddSingleton(dbOptions);

            foreach (var options in dbOptions.Modules)
            {
                var module = modules.FirstOrDefault(m => m.Id.EqualsIgnoreCase(options.Name));
                if (module != null)
                {
                    LoadEntityTypes(module, options);

                    services.AddDbContext(module, options, dbOptions);
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
                var assemblyHelper = new AssemblyHelper();
                var dbContextOptionsAssemblyName = assemblyHelper.GetCurrentAssemblyName().Replace("Integration", "") + dbOptions.Dialect;
                var dbContextOptionsTypeName = dbOptions.Dialect + "DbContextOptions";

                var dbContextOptionsType = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(dbContextOptionsAssemblyName)).GetType($"{dbContextOptionsAssemblyName}.{dbContextOptionsTypeName}");

                //日志工厂
                var loggerFactory = dbOptions.Logging ? services.BuildServiceProvider().GetService<ILoggerFactory>() : null;
                //登录信息
                var loginInfo = services.BuildServiceProvider().GetService<ILoginInfo>();

                var contextOptions = (IDbContextOptions)Activator.CreateInstance(dbContextOptionsType, dbOptions, options, loggerFactory, loginInfo);

                var createDatabaseEvent = module.AssemblyDescriptor.Infrastructure.GetTypes().FirstOrDefault(m => typeof(ICreateDatabaseEvent).IsAssignableFrom(m));
                if (createDatabaseEvent != null)
                {
                    contextOptions.CreateDatabaseEvent = (ICreateDatabaseEvent)Activator.CreateInstance(createDatabaseEvent);
                }
                var dbContext = (IDbContext)Activator.CreateInstance(dbContextType, contextOptions);
                services.AddSingleton(typeof(IDbContext), sp => dbContext);
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
    }
}
