using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Entities;

namespace Nm.Lib.Data.Core
{
    /// <summary>
    /// 仓储抽象类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class RepositoryAbstract<TEntity> : IRepository<TEntity> where TEntity : IEntity, new()
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected readonly IDbContext DbContext;

        /// <summary>
        /// 数据集
        /// </summary>
        protected readonly IDbSet<TEntity> Db;

        protected RepositoryAbstract(IDbContext context)
        {
            DbContext = context;
            Db = context.Set<TEntity>();
        }

        #region ==Transaction==

        public IDbTransaction BeginTransaction()
        {
            return DbContext.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return DbContext.BeginTransaction(isolationLevel);
        }

        #endregion

        #region ==Exists==

        public virtual bool Exists(Expression<Func<TEntity, bool>> where)
        {
            return Db.Find(where).Exists();
        }

        public virtual bool Exists(Expression<Func<TEntity, bool>> where, IDbTransaction transaction)
        {
            return Db.Find(where).UseTran(transaction).Exists();
        }

        public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> where)
        {
            return Db.Find(where).ExistsAsync();
        }

        public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> where, IDbTransaction transaction)
        {
            return Db.Find(where).UseTran(transaction).ExistsAsync();
        }

        public virtual bool Exists(dynamic id)
        {
            return Db.Exists(id);
        }

        public virtual bool Exists(dynamic id, IDbTransaction transaction)
        {
            return Db.Exists(id, transaction);
        }

        public virtual Task<bool> ExistsAsync(dynamic id)
        {
            return Db.ExistsAsync(id);
        }

        public virtual Task<bool> ExistsAsync(dynamic id, IDbTransaction transaction)
        {
            return Db.ExistsAsync(id, transaction);
        }

        #endregion

        #region ==Add==

        public virtual bool Add(TEntity entity)
        {
            if (entity == null)
                return false;

            return Db.Insert(entity);
        }

        public virtual bool Add(TEntity entity, IDbTransaction transaction)
        {
            if (entity == null)
                return false;

            return Db.Insert(entity, transaction);
        }

        public virtual Task<bool> AddAsync(TEntity entity)
        {
            if (entity == null)
                return Task.FromResult(false);

            return Db.InsertAsync(entity);
        }

        public virtual Task<bool> AddAsync(TEntity entity, IDbTransaction transaction)
        {
            if (entity == null)
                return Task.FromResult(false);

            return Db.InsertAsync(entity, transaction);
        }

        public virtual bool Add(List<TEntity> list)
        {
            return Db.BatchInsert(list);
        }

        public virtual bool Add(List<TEntity> list, IDbTransaction transaction)
        {
            return Db.BatchInsert(list, transaction: transaction);
        }
        public virtual Task<bool> AddAsync(List<TEntity> list)
        {
            return Db.BatchInsertAsync(list);
        }

        public virtual Task<bool> AddAsync(List<TEntity> list, IDbTransaction transaction)
        {
            return Db.BatchInsertAsync(list, transaction: transaction);
        }

        #endregion

        #region ==Delete==

        public virtual bool Delete(dynamic id)
        {
            return Db.Delete(id);
        }

        public virtual bool Delete(dynamic id, IDbTransaction transaction)
        {
            return Db.Delete(id, transaction);
        }

        public virtual Task<bool> DeleteAsync(dynamic id)
        {
            return Db.DeleteAsync(id);
        }

        public virtual Task<bool> DeleteAsync(dynamic id, IDbTransaction transaction)
        {
            return Db.DeleteAsync(id, transaction);
        }

        #endregion

        #region ==Update==

        public virtual bool Update(TEntity entity)
        {
            return Db.Update(entity);
        }

        public virtual bool Update(TEntity entity, IDbTransaction transaction)
        {
            return Db.Update(entity, transaction);
        }

        public virtual Task<bool> UpdateAsync(TEntity entity)
        {
            return Db.UpdateAsync(entity);
        }

        public virtual Task<bool> UpdateAsync(TEntity entity, IDbTransaction transaction)
        {
            return Db.UpdateAsync(entity, transaction);
        }

        #endregion

        #region ==Get==

        public virtual TEntity Get(dynamic id)
        {
            return Db.Get(id);
        }

        public virtual TEntity Get(dynamic id, IDbTransaction transaction, bool rowLock = false)
        {
            return Db.Get(id, transaction, null, rowLock);
        }

        public virtual Task<TEntity> GetAsync(dynamic id)
        {
            return Db.GetAsync(id);
        }

        public virtual Task<TEntity> GetAsync(dynamic id, IDbTransaction transaction, bool rowLock = false)
        {
            return Db.GetAsync(id, transaction, null, rowLock);
        }

        protected virtual TEntity Get(Expression<Func<TEntity, bool>> @where)
        {
            return Db.Find(where).First();
        }

        protected virtual TEntity Get(Expression<Func<TEntity, bool>> @where, IDbTransaction transaction)
        {
            return Db.Find(where).UseTran(transaction).First();
        }

        protected virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> @where)
        {
            return Db.Find(where).FirstAsync<TEntity>();
        }

        protected virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> @where, IDbTransaction transaction)
        {
            return Db.Find(where).UseTran(transaction).FirstAsync<TEntity>();
        }

        #endregion

        #region ==GetAll==

        public virtual IList<TEntity> GetAll()
        {
            return Db.Find().ToList<TEntity>();
        }

        public virtual IList<TEntity> GetAll(IDbTransaction transaction)
        {
            return Db.Find().UseTran(transaction).ToList<TEntity>();
        }

        public virtual Task<IList<TEntity>> GetAllAsync()
        {
            return Db.Find().ToListAsync<TEntity>();
        }

        public virtual Task<IList<TEntity>> GetAllAsync(IDbTransaction transaction)
        {
            return Db.Find().UseTran(transaction).ToListAsync<TEntity>();
        }

        #endregion

        #region ==SoftDelete==

        public bool SoftDelete(dynamic id)
        {
            return Db.SoftDelete(id);
        }

        public bool SoftDelete(dynamic id, IDbTransaction transaction)
        {
            return Db.SoftDelete(id, transaction);
        }

        public Task<bool> SoftDeleteAsync(dynamic id)
        {
            return Db.SoftDeleteAsync(id);
        }

        public Task<bool> SoftDeleteAsync(dynamic id, IDbTransaction transaction)
        {
            return Db.SoftDeleteAsync(id, transaction);
        }

        #endregion
    }
}
