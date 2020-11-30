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
    internal class NetSqlQueryable<TEntity, TEntity2> : NetSqlQueryableAbstract, INetSqlQueryable<TEntity, TEntity2>
        where TEntity : IEntity, new()
        where TEntity2 : IEntity, new()
    {
        public NetSqlQueryable(IDbSet dbSet, QueryBody queryBody, Expression<Func<TEntity, TEntity2, bool>> onExpression, JoinType joinType = JoinType.Left, string tableName = null, bool noLock = false)
            : base(dbSet, queryBody)
        {
            Check.NotNull(onExpression, nameof(onExpression), "请输入连接条件");
            var t2 = new QueryJoinDescriptor
            {
                Type = joinType,
                Alias = "T2",
                EntityDescriptor = EntityDescriptorCollection.Get<TEntity2>(),
                On = onExpression,
                NoLock = noLock
            };
            t2.TableName = tableName.NotNull() ? tableName : t2.EntityDescriptor.TableName;
            QueryBody.JoinDescriptors.Add(t2);
        }

        private NetSqlQueryable(IDbSet dbSet, QueryBody queryBody) : base(dbSet, queryBody)
        {
        }

        #region ==UseUow==

        public INetSqlQueryable<TEntity, TEntity2> UseUow(IUnitOfWork uow)
        {
            QueryBody.UseUow(uow);
            return this;
        }

        #endregion

        public INetSqlQueryable<TEntity, TEntity2> OrderBy(string colName)
        {
            return Order(new Sort(colName));
        }

        public INetSqlQueryable<TEntity, TEntity2> OrderByDescending(string colName)
        {
            return Order(new Sort(colName, SortType.Desc));
        }

        public INetSqlQueryable<TEntity, TEntity2> OrderBy<TKey>(Expression<Func<TEntity, TEntity2, TKey>> expression)
        {
            QueryBody.SetOrderBy(expression);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> OrderByDescending<TKey>(Expression<Func<TEntity, TEntity2, TKey>> expression)
        {
            QueryBody.SetOrderBy(expression, SortType.Desc);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> Order(Sort sort)
        {
            QueryBody.SetOrderBy(sort);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> Order<TKey>(Expression<Func<TEntity, TEntity2, TKey>> expression, SortType sortType)
        {
            QueryBody.SetOrderBy(expression, sortType);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> Where(Expression<Func<TEntity, TEntity2, bool>> expression)
        {
            QueryBody.SetWhere(expression);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> Where(string whereSql)
        {
            QueryBody.SetWhere(whereSql);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> WhereIf(bool condition, Expression<Func<TEntity, TEntity2, bool>> expression)
        {
            if (condition)
                Where(expression);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> WhereIf(bool condition, string whereSql)
        {
            if (condition)
                Where(whereSql);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> WhereIf(bool condition, Expression<Func<TEntity, TEntity2, bool>> ifExpression, Expression<Func<TEntity, TEntity2, bool>> elseExpression)
        {
            Where(condition ? ifExpression : elseExpression);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> WhereIf(bool condition, string ifWhereSql, string elseWhereSql)
        {
            Where(condition ? ifWhereSql : elseWhereSql);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> WhereNotNull(string condition, Expression<Func<TEntity, TEntity2, bool>> expression)
        {
            if (condition.NotNull())
                Where(expression);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> WhereNotNull(string condition, string whereSql)
        {
            if (condition.NotNull())
                Where(whereSql);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> WhereNotNull(string condition, Expression<Func<TEntity, TEntity2, bool>> ifExpression, Expression<Func<TEntity, TEntity2, bool>> elseExpression)
        {
            Where(condition.NotNull() ? ifExpression : elseExpression);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> WhereNotNull(string condition, string ifWhereSql, string elseWhereSql)
        {
            Where(condition.NotNull() ? ifWhereSql : elseWhereSql);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> WhereNotNull(object condition, Expression<Func<TEntity, TEntity2, bool>> expression)
        {
            if (condition != null)
                Where(expression);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> WhereNotNull(object condition, string whereSql)
        {
            if (condition != null)
                Where(whereSql);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> WhereNotNull(object condition, Expression<Func<TEntity, TEntity2, bool>> ifExpression, Expression<Func<TEntity, TEntity2, bool>> elseExpression)
        {
            Where(condition != null ? ifExpression : elseExpression);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> WhereNotNull(object condition, string ifWhereSql, string elseWhereSql)
        {
            Where(condition != null ? ifWhereSql : elseWhereSql);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> WhereNotEmpty(Guid condition, Expression<Func<TEntity, TEntity2, bool>> expression)
        {
            if (condition.NotEmpty())
                Where(expression);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> WhereNotEmpty(Guid condition, string whereSql)
        {
            if (condition.NotEmpty())
                Where(whereSql);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> WhereNotEmpty(Guid condition, Expression<Func<TEntity, TEntity2, bool>> ifExpression, Expression<Func<TEntity, TEntity2, bool>> elseExpression)
        {
            Where(condition.NotEmpty() ? ifExpression : elseExpression);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> WhereNotEmpty(Guid condition, string ifWhereSql, string elseWhereSql)
        {
            Where(condition.NotEmpty() ? ifWhereSql : elseWhereSql);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> WhereNotIn<TKey>(Expression<Func<TEntity, TEntity2, TKey>> key, IEnumerable<TKey> list)
        {
            QueryBody.SetWhereNotIn(key, list);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> Where<TKey>(Expression<Func<TEntity, TEntity2, TKey>> key, QueryOperator queryOperator, INetSqlQueryable queryable)
        {
            QueryBody.SetWhere(key, queryOperator, queryable);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> Limit(int skip, int take)
        {
            QueryBody.SetLimit(skip, take);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> Select<TResult>(Expression<Func<TEntity, TEntity2, TResult>> selectExpression)
        {
            QueryBody.SetSelect(selectExpression);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> SelectExclude<TResult>(Expression<Func<TEntity, TEntity2, TResult>> expression)
        {
            QueryBody.SetSelectExclude(expression);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3> LeftJoin<TEntity3>(Expression<Func<TEntity, TEntity2, TEntity3, bool>> onExpression, string tableName = null, bool noLock = true) where TEntity3 : IEntity, new()
        {
            return new NetSqlQueryable<TEntity, TEntity2, TEntity3>(Db, QueryBody, onExpression, JoinType.Left, tableName, noLock);
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3> InnerJoin<TEntity3>(Expression<Func<TEntity, TEntity2, TEntity3, bool>> onExpression, string tableName = null, bool noLock = true) where TEntity3 : IEntity, new()
        {
            return new NetSqlQueryable<TEntity, TEntity2, TEntity3>(Db, QueryBody, onExpression, JoinType.Inner, tableName, noLock);
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3> RightJoin<TEntity3>(Expression<Func<TEntity, TEntity2, TEntity3, bool>> onExpression, string tableName = null, bool noLock = true) where TEntity3 : IEntity, new()
        {
            return new NetSqlQueryable<TEntity, TEntity2, TEntity3>(Db, QueryBody, onExpression, JoinType.Right, tableName, noLock);
        }

        public TResult Max<TResult>(Expression<Func<TEntity, TEntity2, TResult>> expression)
        {
            return base.Max<TResult>(expression);
        }

        public Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TEntity2, TResult>> expression)
        {
            return base.MaxAsync<TResult>(expression);
        }

        public TResult Min<TResult>(Expression<Func<TEntity, TEntity2, TResult>> expression)
        {
            return base.Min<TResult>(expression);
        }

        public Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TEntity2, TResult>> expression)
        {
            return base.MinAsync<TResult>(expression);
        }

        public TResult Sum<TResult>(Expression<Func<TEntity, TEntity2, TResult>> expression)
        {
            return base.Sum<TResult>(expression);
        }

        public Task<TResult> SumAsync<TResult>(Expression<Func<TEntity, TEntity2, TResult>> expression)
        {
            return base.SumAsync<TResult>(expression);
        }

        public TResult Avg<TResult>(Expression<Func<TEntity, TEntity2, TResult>> expression)
        {
            return base.Avg<TResult>(expression);
        }

        public Task<TResult> AvgAsync<TResult>(Expression<Func<TEntity, TEntity2, TResult>> expression)
        {
            return base.AvgAsync<TResult>(expression);
        }

        public IGroupByQueryable2<TResult, TEntity, TEntity2> GroupBy<TResult>(Expression<Func<TEntity, TEntity2, TResult>> expression)
        {
            return new GroupByQueryable2<TResult, TEntity, TEntity2>(Db, QueryBody, QueryBuilder, expression);
        }

        public IGroupByQueryable2<INetSqlGroupingKey2<TEntity, TEntity2>, TEntity, TEntity2> GroupBy()
        {
            return new GroupByQueryable2<INetSqlGroupingKey2<TEntity, TEntity2>, TEntity, TEntity2>(Db, QueryBody, QueryBuilder, null);
        }

        public new IList<TEntity> ToList()
        {
            return ToList<TEntity>();
        }

        public new Task<IList<TEntity>> ToListAsync()
        {
            return ToListAsync<TEntity>();
        }

        public new IList<TEntity> Pagination(Paging paging = null)
        {
            return Pagination<TEntity>(paging);
        }

        public new Task<IList<TEntity>> PaginationAsync(Paging paging = null)
        {
            return PaginationAsync<TEntity>(paging);
        }

        public new TEntity First()
        {
            return First<TEntity>();
        }

        public new Task<TEntity> FirstAsync()
        {
            return FirstAsync<TEntity>();
        }

        public INetSqlQueryable<TEntity, TEntity2> IncludeDeleted()
        {
            QueryBody.FilterDeleted = false;
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> NotFilterTenant()
        {
            QueryBody.FilterTenant = false;
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> Copy()
        {
            return new NetSqlQueryable<TEntity, TEntity2>(Db, QueryBody.Copy());
        }
    }
}
