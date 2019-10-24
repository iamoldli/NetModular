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
        /// 事务
        /// </summary>
        IDbTransaction Transaction { get; }

        /// <summary>
        /// 提交
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚
        /// </summary>
        void Rollback();
    }
}
