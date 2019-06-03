using System.Data;
using Nm.Lib.Data.Abstractions;

namespace Nm.Lib.Data.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbTransaction _transaction;

        private readonly IDbContext _dbContext;

        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            _transaction = _dbContext.BeginTransaction();
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            _transaction = _dbContext.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            _transaction?.Commit();
            _transaction = null;
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction = null;
        }

        public void Dispose()
        {
            //如果不是null，则表示未提交或者出现异常，需要回滚
            _transaction?.Rollback();

            _transaction?.Dispose();
            _dbContext.Connection.Close();
        }
    }

    public class UnitOfWork<TDbContext> : UnitOfWork, IUnitOfWork<TDbContext> where TDbContext : IDbContext
    {
        public UnitOfWork(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
