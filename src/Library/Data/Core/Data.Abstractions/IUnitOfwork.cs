using System;
using System.Data;

namespace Nm.Lib.Data.Abstractions
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 开启事务
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="isolationLevel">隔离级别</param>
        void BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        void Commit();

        /// <summary>
        /// 回滚
        /// </summary>
        void Rollback();
    }

    /// <summary>
    /// 工作单元泛型
    /// </summary>
    public interface IUnitOfWork<TDbContext> : IUnitOfWork where TDbContext : IDbContext
    {

    }
}
