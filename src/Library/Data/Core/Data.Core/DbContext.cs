using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Entities;
using Nm.Lib.Utils.Core.Helpers;
using IsolationLevel = System.Data.IsolationLevel;

namespace Nm.Lib.Data.Core
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public abstract class DbContext : IDbContext
    {
        #region ==属性==

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

        protected DbContext(IDbContextOptions options)
        {
            Options = options;
            LoginInfo = Options.LoginInfo;

            if (options.DbOptions.CreateDatabase)
            {
                options.CreateDatabaseEvent?.Before(this).GetAwaiter().GetResult();

                options.SqlAdapter.CreateDatabase(EntityDescriptorCollection.Get(options.DbModuleOptions.Name));

                options.CreateDatabaseEvent?.After(this).GetAwaiter().GetResult();
            }
        }

        #endregion

        #region ==方法==

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
                    var connString = "";
                    foreach (var param in c.ConnectionString.Split(';'))
                    {
                        var temp = param.Split('=');
                        var key = temp[0];
                        if (key.Equals("Data Source", StringComparison.OrdinalIgnoreCase) || key.Equals("DataSource", StringComparison.OrdinalIgnoreCase))
                        {
                            connString = temp[1];
                            break;
                        }
                    }

                    sql.AppendFormat("ATTACH DATABASE '{0}' as '{1}';", connString, conn.Database);
                }

                conn.ExecuteAsync(sql.ToString());
            }

            return conn;
        }

        public IDbTransaction BeginTransaction()
        {
            if (Options.SqlAdapter.SqlDialect == Abstractions.Enums.SqlDialect.SQLite)
                return null;

            var conn = NewConnection();
            conn.Open();
            return conn.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            if (Options.SqlAdapter.SqlDialect == Abstractions.Enums.SqlDialect.SQLite)
                return null;

            var conn = NewConnection();
            conn.Open();
            return conn.BeginTransaction(isolationLevel);
        }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : IEntity, new()
        {
            return new DbSet<TEntity>(this);
        }

        #endregion
    }
}