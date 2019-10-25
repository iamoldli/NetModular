using System.Data;
using NetModular.Lib.Data.Abstractions;

namespace NetModular.Lib.Data.Core
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        public IDbTransaction Transaction { get; private set; }

        public void Commit()
        {
            if (Transaction != null)
            {
                Transaction.Commit();
                Transaction = null;
            }
        }

        public void Rollback()
        {
            Transaction?.Rollback();
        }

        public void Dispose()
        {
            Rollback();
        }
    }
}
