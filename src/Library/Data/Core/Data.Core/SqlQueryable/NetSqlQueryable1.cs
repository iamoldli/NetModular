using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Entities;
using Nm.Lib.Data.Abstractions.Enums;
using Nm.Lib.Data.Abstractions.Pagination;
using Nm.Lib.Data.Abstractions.SqlQueryable;
using Nm.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable;
using Nm.Lib.Data.Core.SqlQueryable.GroupByQueryable;
using Nm.Lib.Data.Core.SqlQueryable.Internal;

namespace Nm.Lib.Data.Core.SqlQueryable
{
    internal class NetSqlQueryable<TEntity> : NetSqlQueryableAbstract, INetSqlQueryable<TEntity> where TEntity : IEntity, new()
    {
        public NetSqlQueryable(IDbSet<TEntity> dbSet, Expression<Func<TEntity, bool>> whereExpression) : base(dbSet, new QueryBody(dbSet.DbContext.Options.SqlAdapter))
        {
            QueryBody.JoinDescriptors.Add(new QueryJoinDescriptor
            {
                Type = JoinType.UnKnown,
                Alias = "T1",
                EntityDescriptor = EntityDescriptorCollection.Get<TEntity>(),
            });
            QueryBody.WhereDelegateType = typeof(Func<,>).MakeGenericType(typeof(TEntity), typeof(bool));

            Where(whereExpression);
        }

        #region ==Sort==

        public INetSqlQueryable<TEntity> OrderBy(string colName)
        {
            return Order(new Sort(colName));
        }

        public INetSqlQueryable<TEntity> OrderByDescending(string colName)
        {
            return Order(new Sort(colName, SortType.Desc));
        }

        public INetSqlQueryable<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> expression)
        {
            QueryBody.SetOrderBy(expression);
            return this;
        }

        public INetSqlQueryable<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> expression)
        {
            QueryBody.SetOrderBy(expression, SortType.Desc);
            return this;
        }

        public INetSqlQueryable<TEntity> Order(Sort sort)
        {
            QueryBody.SetOrderBy(sort);
            return this;
        }

        public INetSqlQueryable<TEntity> Order<TKey>(Expression<Func<TEntity, TKey>> expression, SortType sortType)
        {
            QueryBody.SetOrderBy(expression, sortType);
            return this;
        }

        #endregion

        #region ==Where==

        public INetSqlQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            QueryBody.SetWhere(expression);
            return this;
        }

        public INetSqlQueryable<TEntity> WhereIf(bool ifCondition, Expression<Func<TEntity, bool>> expression)
        {
            if (ifCondition)
                Where(expression);

            return this;
        }

        #endregion

        #region ==Limit==

        public INetSqlQueryable<TEntity> Limit(int skip, int take)
        {
            QueryBody.SetLimit(skip, take);
            return this;
        }

        #endregion

        #region ==Select==

        public INetSqlQueryable<TEntity> Select<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            QueryBody.SetSelect(expression);
            return this;
        }

        #endregion

        #region ==Join==

        public INetSqlQueryable<TEntity, TEntity2> LeftJoin<TEntity2>(Expression<Func<TEntity, TEntity2, bool>> onExpression) where TEntity2 : IEntity, new()
        {
            return new NetSqlQueryable<TEntity, TEntity2>(Db, QueryBody, onExpression);
        }

        public INetSqlQueryable<TEntity, TEntity2> InnerJoin<TEntity2>(Expression<Func<TEntity, TEntity2, bool>> onExpression) where TEntity2 : IEntity, new()
        {
            return new NetSqlQueryable<TEntity, TEntity2>(Db, QueryBody, onExpression, JoinType.Inner);
        }

        public INetSqlQueryable<TEntity, TEntity2> RightJoin<TEntity2>(Expression<Func<TEntity, TEntity2, bool>> onExpression) where TEntity2 : IEntity, new()
        {
            return new NetSqlQueryable<TEntity, TEntity2>(Db, QueryBody, onExpression, JoinType.Right);
        }

        #endregion

        #region ==Delete==

        public bool Delete()
        {
            DeleteWithAffectedNum();
            return true;
        }

        public async Task<bool> DeleteAsync()
        {
            await DeleteWithAffectedNumAsync();
            return true;
        }

        public int DeleteWithAffectedNum()
        {
            var sql = QueryBuilder.DeleteSqlBuild(Db.EntityDescriptor.TableName, out QueryParameters parameters);

            return Db.Execute(sql, parameters.Parse());
        }

        public Task<int> DeleteWithAffectedNumAsync()
        {
            var sql = QueryBuilder.DeleteSqlBuild(Db.EntityDescriptor.TableName, out QueryParameters parameters);

            return Db.ExecuteAsync(sql, parameters.Parse());
        }

        #endregion

        #region ==SoftDelete==

        public bool SoftDelete()
        {
            SoftDeleteWithAffectedNum();
            return true;
        }

        public async Task<bool> SoftDeleteAsync()
        {
            await SoftDeleteWithAffectedNumAsync();
            return true;
        }

        public int SoftDeleteWithAffectedNum()
        {
            var sql = QueryBuilder.SoftDeleteSqlBuild(Db.EntityDescriptor.TableName, out QueryParameters parameters);

            return Db.Execute(sql, parameters.Parse());
        }

        public Task<int> SoftDeleteWithAffectedNumAsync()
        {
            var sql = QueryBuilder.SoftDeleteSqlBuild(Db.EntityDescriptor.TableName, out QueryParameters parameters);

            return Db.ExecuteAsync(sql, parameters.Parse());
        }

        #endregion

        #region ==Update==

        public bool Update(Expression<Func<TEntity, TEntity>> expression, bool setModifiedBy = true)
        {
            UpdateWithAffectedNum(expression, setModifiedBy);
            return true;
        }

        public async Task<bool> UpdateAsync(Expression<Func<TEntity, TEntity>> expression, bool setModifiedBy = true)
        {
            await UpdateWithAffectedNumAsync(expression, setModifiedBy);
            return true;
        }

        public int UpdateWithAffectedNum(Expression<Func<TEntity, TEntity>> expression, bool setModifiedBy = true)
        {
            QueryBody.Update = expression;
            QueryBody.SetModifiedBy = setModifiedBy;
            var sql = QueryBuilder.UpdateSqlBuild(Db.EntityDescriptor.TableName, out QueryParameters parameters);

            return Db.Execute(sql, parameters.Parse());
        }

        public Task<int> UpdateWithAffectedNumAsync(Expression<Func<TEntity, TEntity>> expression, bool setModifiedBy = true)
        {
            QueryBody.Update = expression;
            QueryBody.SetModifiedBy = setModifiedBy;
            var sql = QueryBuilder.UpdateSqlBuild(Db.EntityDescriptor.TableName, out QueryParameters parameters);

            return Db.ExecuteAsync(sql, parameters.Parse());
        }

        #endregion

        #region ==Max==

        public TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return base.Max<TResult>(expression);
        }

        public Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return base.MaxAsync<TResult>(expression);
        }

        #endregion

        #region ==Min==


        public TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return base.Min<TResult>(expression);
        }

        public Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return base.MinAsync<TResult>(expression);
        }

        public TResult Sum<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return base.Sum<TResult>(expression);
        }

        public Task<TResult> SumAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return base.SumAsync<TResult>(expression);
        }

        public TResult Avg<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return base.Avg<TResult>(expression);
        }

        public Task<TResult> AvgAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return base.AvgAsync<TResult>(expression);
        }

        #endregion

        #region ==GroupBy==

        public IGroupByQueryable1<TResult, TEntity> GroupBy<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return new GroupByQueryable1<TResult, TEntity>(Db, QueryBody, QueryBuilder, expression);
        }

        #endregion

        #region ==ToList==

        public new IList<TEntity> ToList()
        {
            return ToList<TEntity>();
        }

        public new Task<IList<TEntity>> ToListAsync()
        {
            return ToListAsync<TEntity>();
        }

        #endregion

        #region ==Pagination==

        public new IList<TEntity> Pagination(Paging paging = null)
        {
            return Pagination<TEntity>(paging);
        }

        public new Task<IList<TEntity>> PaginationAsync(Paging paging = null)
        {
            return PaginationAsync<TEntity>(paging);
        }

        #endregion

        #region ==First==

        public new TEntity First()
        {
            return First<TEntity>();
        }

        public new Task<TEntity> FirstAsync()
        {
            return FirstAsync<TEntity>();
        }

        #endregion
    }
}
