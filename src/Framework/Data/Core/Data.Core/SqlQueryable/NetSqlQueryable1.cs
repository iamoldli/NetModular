using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Abstractions.Enums;
using NetModular.Lib.Data.Abstractions.Pagination;
using NetModular.Lib.Data.Abstractions.SqlQueryable;
using NetModular.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable;
using NetModular.Lib.Data.Core.SqlQueryable.GroupByQueryable;
using NetModular.Lib.Data.Core.SqlQueryable.Internal;

namespace NetModular.Lib.Data.Core.SqlQueryable
{
    internal class NetSqlQueryable<TEntity> : NetSqlQueryableAbstract, INetSqlQueryable<TEntity> where TEntity : IEntity, new()
    {
        public NetSqlQueryable(IDbSet<TEntity> dbSet, Expression<Func<TEntity, bool>> whereExpression, string tableName = null, bool noLock = true) : base(dbSet, new QueryBody(dbSet.DbContext.Options.SqlAdapter))
        {
            QueryBody.JoinDescriptors.Add(new QueryJoinDescriptor
            {
                Type = JoinType.UnKnown,
                Alias = "T1",
                EntityDescriptor = EntityDescriptorCollection.Get<TEntity>(),
                TableName = tableName.NotNull() ? tableName : Db.EntityDescriptor.TableName,
                NoLock = noLock
            });

            Where(whereExpression);
        }

        private NetSqlQueryable(IDbSet dbSet, QueryBody queryBody) : base(dbSet, queryBody)
        {

        }

        #region ==UseUow==

        public INetSqlQueryable<TEntity> UseUow(IUnitOfWork uow)
        {
            QueryBody.UseUow(uow);
            return this;
        }

        #endregion

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

        public INetSqlQueryable<TEntity> Where(string whereSql)
        {
            QueryBody.SetWhere(whereSql);
            return this;
        }

        public INetSqlQueryable<TEntity> WhereIf(bool condition, Expression<Func<TEntity, bool>> expression)
        {
            if (condition)
                Where(expression);

            return this;
        }

        public INetSqlQueryable<TEntity> WhereIf(bool condition, string whereSql)
        {
            if (condition)
                Where(whereSql);

            return this;
        }

        public INetSqlQueryable<TEntity> WhereIf(bool condition, Expression<Func<TEntity, bool>> ifExpression, Expression<Func<TEntity, bool>> elseExpression)
        {
            Where(condition ? ifExpression : elseExpression);
            return this;
        }

        public INetSqlQueryable<TEntity> WhereIf(bool condition, string ifWhereSql, string elseWhereSql)
        {
            Where(condition ? ifWhereSql : elseWhereSql);
            return this;
        }

        public INetSqlQueryable<TEntity> WhereNotNull(string condition, Expression<Func<TEntity, bool>> expression)
        {
            if (condition.NotNull())
                Where(expression);

            return this;
        }

        public INetSqlQueryable<TEntity> WhereNotNull(string condition, string whereSql)
        {
            if (condition.NotNull())
                Where(whereSql);

            return this;
        }

        public INetSqlQueryable<TEntity> WhereNotNull(string condition, Expression<Func<TEntity, bool>> ifExpression, Expression<Func<TEntity, bool>> elseExpression)
        {
            Where(condition.NotNull() ? ifExpression : elseExpression);
            return this;
        }

        public INetSqlQueryable<TEntity> WhereNotNull(string condition, string ifWhereSql, string elseWhereSql)
        {
            Where(condition.NotNull() ? ifWhereSql : elseWhereSql);
            return this;
        }

        public INetSqlQueryable<TEntity> WhereNotNull(object condition, Expression<Func<TEntity, bool>> expression)
        {
            if (condition != null)
                Where(expression);

            return this;
        }

        public INetSqlQueryable<TEntity> WhereNotNull(object condition, string whereSql)
        {
            if (condition != null)
                Where(whereSql);

            return this;
        }

        public INetSqlQueryable<TEntity> WhereNotNull(object condition, Expression<Func<TEntity, bool>> ifExpression, Expression<Func<TEntity, bool>> elseExpression)
        {
            Where(condition != null ? ifExpression : elseExpression);
            return this;
        }

        public INetSqlQueryable<TEntity> WhereNotNull(object condition, string ifWhereSql, string elseWhereSql)
        {
            Where(condition != null ? ifWhereSql : elseWhereSql);
            return this;
        }

        public INetSqlQueryable<TEntity> WhereNotEmpty(Guid condition, Expression<Func<TEntity, bool>> expression)
        {
            if (condition.NotEmpty())
                Where(expression);

            return this;
        }

        public INetSqlQueryable<TEntity> WhereNotEmpty(Guid condition, string whereSql)
        {
            if (condition.NotEmpty())
                Where(whereSql);

            return this;
        }

        public INetSqlQueryable<TEntity> WhereNotEmpty(Guid condition, Expression<Func<TEntity, bool>> ifExpression, Expression<Func<TEntity, bool>> elseExpression)
        {
            Where(condition.NotEmpty() ? ifExpression : elseExpression);
            return this;
        }

        public INetSqlQueryable<TEntity> WhereNotEmpty(Guid condition, string ifWhereSql, string elseWhereSql)
        {
            Where(condition.NotEmpty() ? ifWhereSql : elseWhereSql);
            return this;
        }

        public INetSqlQueryable<TEntity> WhereNotIn<TKey>(Expression<Func<TEntity, TKey>> key, IEnumerable<TKey> list)
        {
            QueryBody.SetWhereNotIn(key, list);
            return this;
        }

        public INetSqlQueryable<TEntity> Where<TKey>(Expression<Func<TEntity, TKey>> key, QueryOperator queryOperator, INetSqlQueryable queryable)
        {
            QueryBody.SetWhere(key, queryOperator, queryable);
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

        public INetSqlQueryable<TEntity> SelectExclude<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            QueryBody.SetSelectExclude(expression);
            return this;
        }

        #endregion

        #region ==Join==

        public INetSqlQueryable<TEntity, TEntity2> LeftJoin<TEntity2>(Expression<Func<TEntity, TEntity2, bool>> onExpression, string tableName = null, bool noLock = true) where TEntity2 : IEntity, new()
        {
            return new NetSqlQueryable<TEntity, TEntity2>(Db, QueryBody, onExpression, JoinType.Left, tableName, noLock);
        }

        public INetSqlQueryable<TEntity, TEntity2> InnerJoin<TEntity2>(Expression<Func<TEntity, TEntity2, bool>> onExpression, string tableName = null, bool noLock = true) where TEntity2 : IEntity, new()
        {
            return new NetSqlQueryable<TEntity, TEntity2>(Db, QueryBody, onExpression, JoinType.Inner, tableName, noLock);
        }

        public INetSqlQueryable<TEntity, TEntity2> RightJoin<TEntity2>(Expression<Func<TEntity, TEntity2, bool>> onExpression, string tableName = null, bool noLock = true) where TEntity2 : IEntity, new()
        {
            return new NetSqlQueryable<TEntity, TEntity2>(Db, QueryBody, onExpression, JoinType.Right, tableName, noLock);
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
            var sql = QueryBuilder.DeleteSqlBuild(out IQueryParameters parameters);

            return Db.Execute(sql, parameters.Parse(), QueryBody.Uow);
        }

        public Task<int> DeleteWithAffectedNumAsync()
        {
            var sql = QueryBuilder.DeleteSqlBuild(out IQueryParameters parameters);

            return Db.ExecuteAsync(sql, parameters.Parse(), QueryBody.Uow);
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
            var sql = QueryBuilder.SoftDeleteSqlBuild(out IQueryParameters parameters);

            return Db.Execute(sql, parameters.Parse(), QueryBody.Uow);
        }

        public Task<int> SoftDeleteWithAffectedNumAsync()
        {
            var sql = QueryBuilder.SoftDeleteSqlBuild(out IQueryParameters parameters);

            return Db.ExecuteAsync(sql, parameters.Parse(), QueryBody.Uow);
        }

        #endregion

        #region ==Update==

        public bool Update(Expression<Func<TEntity, TEntity>> expression, bool setModifiedBy = true)
        {
            UpdateWithAffectedNum(expression, setModifiedBy);
            return true;
        }

        public bool Update(string updateSql, bool setModifiedBy = true, object parameterObject = null)
        {
            UpdateWithAffectedNum(null, setModifiedBy, updateSql, parameterObject);
            return true;
        }

        public async Task<bool> UpdateAsync(Expression<Func<TEntity, TEntity>> expression, bool setModifiedBy = true)
        {
            await UpdateWithAffectedNumAsync(expression, setModifiedBy);
            return true;
        }

        public async Task<bool> UpdateAsync(string updateSql, bool setModifiedBy = true, object parameterObject = null)
        {
            await UpdateWithAffectedNumAsync(null, setModifiedBy, updateSql, parameterObject);
            return true;
        }

        public int UpdateWithAffectedNum(Expression<Func<TEntity, TEntity>> expression, bool setModifiedBy = true, string updateSql = null, object parameterObject = null)
        {
            if (updateSql.IsNull())
            {
                QueryBody.Update = expression;
            }
            else
            {
                QueryBody.UpdateSql = updateSql;
            }

            QueryBody.SetModifiedBy = setModifiedBy;
            var sql = QueryBuilder.UpdateSqlBuild(out IQueryParameters parameters);
            var param = parameters.Parse();
            if (updateSql.IsNull() && parameterObject != null)
            {
                param.AddDynamicParams(parameterObject);
            }
            return Db.Execute(sql, param, QueryBody.Uow);
        }

        public Task<int> UpdateWithAffectedNumAsync(Expression<Func<TEntity, TEntity>> expression, bool setModifiedBy = true, string updateSql = null, object parameterObject = null)
        {
            if (updateSql.IsNull())
            {
                QueryBody.Update = expression;
            }
            else
            {
                QueryBody.UpdateSql = updateSql;
            }

            QueryBody.SetModifiedBy = setModifiedBy;
            var sql = QueryBuilder.UpdateSqlBuild(out IQueryParameters parameters);
            var param = parameters.Parse();
            if (updateSql.IsNull() && parameterObject != null)
            {
                param.AddDynamicParams(parameterObject);
            }
            return Db.ExecuteAsync(sql, param, QueryBody.Uow);
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

        #endregion

        #region ==Sum==

        public TResult Sum<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return base.Sum<TResult>(expression);
        }

        public Task<TResult> SumAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return base.SumAsync<TResult>(expression);
        }

        #endregion

        #region ==Avg==

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

        public IGroupByQueryable1<INetSqlGroupingKey1<TEntity>, TEntity> GroupBy()
        {
            return new GroupByQueryable1<INetSqlGroupingKey1<TEntity>, TEntity>(Db, QueryBody, QueryBuilder, null);
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

        #region ==IncludeDeleted==

        public INetSqlQueryable<TEntity> IncludeDeleted()
        {
            QueryBody.FilterDeleted = false;
            return this;
        }

        public INetSqlQueryable<TEntity> NotFilterTenant()
        {
            QueryBody.FilterTenant = false;
            return this;
        }

        #endregion

        #region ==Copy==

        public INetSqlQueryable<TEntity> Copy()
        {
            return new NetSqlQueryable<TEntity>(Db, QueryBody.Copy());
        }

        #endregion
    }
}
