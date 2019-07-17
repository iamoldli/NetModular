using System;
using System.Data;
using System.Text;
using Dapper;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Entities;

namespace Nm.Lib.Data.Core
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public abstract class DbContext : IDbContext
    {
        private static readonly object Lock = new object();

        #region ==属性==

        /// <summary>
        /// 登录信息
        /// </summary>
        public ILoginInfo LoginInfo { get; }

        /// <summary>
        /// 数据库上下文配置项
        /// </summary>
        public IDbContextOptions Options { get; }

        /// <summary>
        /// 数据库连接
        /// </summary>
        public IDbConnection Connection { get; private set; }

        /// <summary>
        /// 事务
        /// </summary>
        public IDbTransaction Transaction { get; set; }

        #endregion

        #region ==构造函数==

        protected DbContext(IDbContextOptions options)
        {
            Options = options;
            LoginInfo = Options.LoginInfo;
        }

        #endregion

        #region ==方法==

        public IDbTransaction BeginTransaction()
        {
            if (Options.SqlAdapter.SqlDialect == Abstractions.Enums.SqlDialect.SQLite)
                return null;

            Open();
            return Transaction = Transaction ?? Connection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            if (Options.SqlAdapter.SqlDialect == Abstractions.Enums.SqlDialect.SQLite)
                return null;

            Open();
            return Transaction = Transaction ?? Connection.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// 打开连接
        /// </summary>
        public IDbConnection Open()
        {
            //加个锁，防止并发时异常
            lock (Lock)
            {
                if (Connection == null)
                    Connection = Options.OpenConnection();

                if (Connection.State != ConnectionState.Open)
                {
                    Connection.Open();

                    //SQLite跨数据库访问需要附加
                    if (Options.SqlAdapter.SqlDialect == Abstractions.Enums.SqlDialect.SQLite)
                    {
                        var sql = new StringBuilder();
                        foreach (var conn in Options.DbOptions.Connections)
                        {
                            var connString = "";
                            foreach (var param in conn.ConnString.Split(';'))
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

                        Connection.ExecuteAsync(sql.ToString());
                    }
                }
            }

            return Connection;
        }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : IEntity, new()
        {
            return new DbSet<TEntity>(this);
        }

        #endregion

        public void Dispose()
        {
            Transaction?.Dispose();
            Connection?.Dispose();
        }
    }
}