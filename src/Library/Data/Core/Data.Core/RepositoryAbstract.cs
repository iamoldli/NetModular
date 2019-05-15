using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Entities;

namespace NetModular.Lib.Data.Core
{
    /// <summary>
    /// 仓储抽象类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class RepositoryAbstract<TEntity> : IRepository<TEntity> where TEntity : IEntity, new()
    {
        /// <summary>
        /// 数据集
        /// </summary>
        protected readonly IDbSet<TEntity> Db;

        protected RepositoryAbstract(IDbContext context)
        {
            Db = context.Set<TEntity>();
        }

        #region ==Exists==

        public bool Exists(Expression<Func<TEntity, bool>> where)
        {
            return Db.Find(where).Exists();
        }

        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> where)
        {
            return Db.Find(where).ExistsAsync();
        }

        public bool Exists(dynamic id)
        {
            return Db.Exists(id);
        }

        public Task<bool> ExistsAsync(dynamic id)
        {
            return Db.ExistsAsync(id);
        }

        #endregion

        #region ==Add==

        public virtual bool Add(TEntity entity)
        {
            if (entity == null)
                return false;

            return Db.Insert(entity);
        }

        public virtual Task<bool> AddAsync(TEntity entity)
        {
            if (entity == null)
                return Task.FromResult(false);

            return Db.InsertAsync(entity);
        }

        public virtual bool Add(List<TEntity> list)
        {
            return Db.BatchInsert(list);
        }

        public virtual Task<bool> AddAsync(List<TEntity> list)
        {
            return Db.BatchInsertAsync(list);
        }

        #endregion

        #region ==Delete==

        public virtual bool Delete(dynamic id)
        {
            return Db.Delete(id);
        }

        public virtual Task<bool> DeleteAsync(dynamic id)
        {
            return Db.DeleteAsync(id);
        }

        #endregion

        #region ==Update==

        public virtual bool Update(TEntity entity)
        {
            return Db.Update(entity);
        }

        public virtual Task<bool> UpdateAsync(TEntity entity)
        {
            return Db.UpdateAsync(entity);
        }

        #endregion

        #region ==Get==

        public virtual TEntity Get(dynamic id)
        {
            return Db.Get(id);
        }

        public virtual Task<TEntity> GetAsync(dynamic id)
        {
            return Db.GetAsync(id);
        }

        protected virtual TEntity Get(Expression<Func<TEntity, bool>> @where)
        {
            return Db.Find(where).First();
        }

        protected virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> @where)
        {
            return Db.Find(where).FirstAsync<TEntity>();
        }

        #endregion

        #region ==GetAll==

        public virtual IList<TEntity> GetAll()
        {
            return Db.Find().ToList<TEntity>();
        }

        public virtual Task<IList<TEntity>> GetAllAsync()
        {
            return Db.Find().ToListAsync<TEntity>();
        }

        #endregion

        #region ==SoftDelete==

        public bool SoftDelete(dynamic id)
        {
            return Db.SoftDelete(id);
        }

        public Task<bool> SoftDeleteAsync(dynamic id)
        {
            return Db.SoftDeleteAsync(id);
        }

        #endregion
    }
}
