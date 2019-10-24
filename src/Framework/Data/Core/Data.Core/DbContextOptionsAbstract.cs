using System;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Logging;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Entities;
using Nm.Lib.Data.Abstractions.Options;
using Nm.Lib.Data.Core.Entities;
using Nm.Lib.Utils.Core.Extensions;

namespace Nm.Lib.Data.Core
{
    public abstract class DbContextOptionsAbstract : IDbContextOptions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbOptions"></param>
        /// <param name="options"></param>
        /// <param name="sqlAdapter">数据库适配器</param>
        /// <param name="loggerFactory">日志工厂</param>
        /// <param name="loginInfo">登录信息</param>
        protected DbContextOptionsAbstract(DbOptions dbOptions, DbModuleOptions options, ISqlAdapter sqlAdapter, ILoggerFactory loggerFactory, ILoginInfo loginInfo)
        {
            if (options.Name.IsNull())
                throw new ArgumentNullException(nameof(options.Name), "数据库连接名称未配置");

            DbOptions = dbOptions;
            DbModuleOptions = options;
            SqlAdapter = sqlAdapter;
            LoggerFactory = loggerFactory;
            LoginInfo = loginInfo;

            if (options.EntityTypes != null && options.EntityTypes.Any())
            {
                foreach (var entityType in options.EntityTypes)
                {
                    EntityDescriptorCollection.Add(new EntityDescriptor(options.Name, entityType, sqlAdapter, new EntitySqlBuilder()));
                }
            }
        }

        public DbModuleOptions DbModuleOptions { get; }

        public IDatabaseCreateEvents DatabaseCreateEvents { get; set; }

        public ISqlAdapter SqlAdapter { get; }

        public abstract IDbConnection NewConnection();

        public ILoggerFactory LoggerFactory { get; }

        public ILoginInfo LoginInfo { get; set; }

        public DbOptions DbOptions { get; }
    }
}
