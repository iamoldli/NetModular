using System.Data;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Entities;

namespace NetModular.Lib.Data.Core
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public abstract class DbContext : IDbContext
    {
        #region ==属性==

        /// <summary>
        /// 当前登录账户编号
        /// </summary>
        public string AccountId => Options.HttpContextAccessor?.HttpContext.User.FindFirst("id")?.Value;

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
        }

        #endregion

        #region ==方法==

        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            Open();
            return Transaction = Transaction ?? Connection.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// 打开连接
        /// </summary>
        public IDbConnection Open()
        {
            if (Connection == null)
                Connection = Options.OpenConnection();

            if (Connection.State != ConnectionState.Open)
                Connection.Open();

            return Connection;
        }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : IEntity, new()
        {
            return new DbSet<TEntity>(this);
        }

        #endregion

        public void Dispose()
        {
            Connection?.Dispose();
            Transaction?.Dispose();
        }
    }
}