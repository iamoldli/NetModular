using System.Data;
using NetModular.Lib.Data.Abstractions;

namespace NetModular.Lib.Data.Core
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IDbTransaction transaction, IDbConnection dbConnection)
        {
            Transaction = transaction;
            DbConnection = dbConnection;
        }

        public IDbTransaction Transaction { get; private set; }
        public IDbConnection DbConnection { get; private set; }

        public void Commit()
        {
            if (Transaction != null)
            {
                Transaction.Commit();
                DbConnection?.Close();
                Transaction = null;
            }
        }

        public void Rollback()
        {
            Transaction?.Rollback();
            DbConnection?.Close();
        }

        public void Dispose()
        {
            Rollback();
        }
    }
}
