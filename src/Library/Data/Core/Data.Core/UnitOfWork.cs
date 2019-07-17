using System.Data;
using Nm.Lib.Data.Abstractions;

namespace Nm.Lib.Data.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _dbContext;

        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            _dbContext.BeginTransaction();
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            _dbContext.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            if (_dbContext.Transaction != null)
            {
                _dbContext.Transaction.Commit();
                _dbContext.Transaction = null;
            }
        }

        public void Rollback()
        {
            if (_dbContext.Transaction != null)
            {
                _dbContext.Transaction.Rollback();
                _dbContext.Transaction = null;
            }
        }

        public void Dispose()
        {
            //如果不是null，则表示未提交或者出现异常，需要回滚
            Rollback();

            if (_dbContext.Connection != null && _dbContext.Connection.State != ConnectionState.Closed)
            {
                _dbContext.Connection.Close();
            }
        }
    }

    public class UnitOfWork<TDbContext> : UnitOfWork, IUnitOfWork<TDbContext> where TDbContext : IDbContext
    {
        public UnitOfWork(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
