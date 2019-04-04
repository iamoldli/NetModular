using System.Data;
using NetModular.Lib.Data.Abstractions;

namespace NetModular.Lib.Data.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbTransaction _transaction;

        private readonly IDbContext _dbContext;

        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _transaction = _dbContext.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            _transaction?.Commit();
            _transaction = null;
            _dbContext.Transaction = null;
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction = null;
            _dbContext.Transaction = null;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }
    }

    public class UnitOfWork<TDbContext> : UnitOfWork, IUnitOfWork<TDbContext> where TDbContext : IDbContext
    {
        public UnitOfWork(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
