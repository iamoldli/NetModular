using System;
using System.Data;
using System.IO;
using System.Text;
using Dapper;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Utils.Core.Extensions;
using IsolationLevel = System.Data.IsolationLevel;

namespace NetModular.Lib.Data.Core
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public abstract class DbContext : IDbContext
    {
        #region ==属性==

        /// <summary>
        /// 服务提供器
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 登录信息
        /// </summary>
        public ILoginInfo LoginInfo { get; }

        /// <summary>
        /// 数据库上下文配置项
        /// </summary>
        public IDbContextOptions Options { get; }

        #endregion

        #region ==构造函数==

        protected DbContext(IDbContextOptions options, IServiceProvider serviceProvider)
        {
            Options = options;
            ServiceProvider = serviceProvider;
            LoginInfo = Options.LoginInfo;

            if (Options.DbOptions.CreateDatabase)
            {
                if (Options.DatabaseCreateEvents != null)
                {
                    Options.DatabaseCreateEvents.DbContext = this;
                }

                CreateDatabase();
            }
        }

        #endregion

        #region ==方法==

        /// <summary>
        /// 创建新的连接
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public IDbConnection NewConnection(IDbTransaction transaction = null)
        {
            if (transaction != null)
                return transaction.Connection;

            var conn = Options.NewConnection();

            //SQLite跨数据库访问需要附加
            if (Options.SqlAdapter.SqlDialect == Abstractions.Enums.SqlDialect.SQLite)
            {
                conn.Open();

                var sql = new StringBuilder();
                foreach (var c in Options.DbOptions.Modules)
                {
                    string dbFilePath = Path.Combine(AppContext.BaseDirectory, "Db");
                    if (Options.DbOptions.Server.NotNull())
                    {
                        dbFilePath = Path.GetFullPath(Options.DbOptions.Server);
                    }
                    dbFilePath = Path.Combine(dbFilePath, c.Database) + ".db";

                    sql.AppendFormat("ATTACH DATABASE '{0}' as '{1}';", dbFilePath, c.Database);
                }

                conn.ExecuteAsync(sql.ToString()).Wait();
            }

            return conn;
        }

        public IUnitOfWork NewUnitOfWork()
        {
            //SQLite数据库开启事务时会报 database is locked 错误
            if (Options.SqlAdapter.SqlDialect == Abstractions.Enums.SqlDialect.SQLite)
                return new UnitOfWork(null);

            var con = NewConnection();
            con.Open();
            return new UnitOfWork(con.BeginTransaction());
        }

        public IUnitOfWork NewUnitOfWork(IsolationLevel isolationLevel)
        {
            //SQLite数据库开启事务时会报 database is locked 错误
            if (Options.SqlAdapter.SqlDialect == Abstractions.Enums.SqlDialect.SQLite)
                return new UnitOfWork(null);

            var con = NewConnection();
            con.Open();
            return new UnitOfWork(con.BeginTransaction(isolationLevel));
        }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : IEntity, new()
        {
            return new DbSet<TEntity>(this);
        }

        public void CreateDatabase()
        {
            Options.SqlAdapter.CreateDatabase(EntityDescriptorCollection.Get(Options.DbModuleOptions.Name), Options.DatabaseCreateEvents);
        }

        #endregion
    }
}