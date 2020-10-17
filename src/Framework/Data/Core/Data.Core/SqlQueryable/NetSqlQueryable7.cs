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
    internal class NetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>
        : NetSqlQueryableAbstract, INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>
        where TEntity : IEntity, new()
        where TEntity2 : IEntity, new()
        where TEntity3 : IEntity, new()
        where TEntity4 : IEntity, new()
        where TEntity5 : IEntity, new()
        where TEntity6 : IEntity, new()
        where TEntity7 : IEntity, new()
    {
        public NetSqlQueryable(IDbSet dbSet, QueryBody queryBody, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, bool>> onExpression, JoinType joinType = JoinType.Left, string tableName = null, bool noLock = true)
            : base(dbSet, queryBody)
        {
            Check.NotNull(onExpression, nameof(onExpression), "请输入连接条件");

            var t7 = new QueryJoinDescriptor
            {
                Type = joinType,
                Alias = "T7",
                EntityDescriptor = EntityDescriptorCollection.Get<TEntity7>(),
                On = onExpression,
                NoLock = noLock
            };
            t7.TableName = tableName.NotNull() ? tableName : t7.EntityDescriptor.TableName;
            QueryBody.JoinDescriptors.Add(t7);


            QueryBody.WhereDelegateType = typeof(Func<,,,,,,,>).MakeGenericType(typeof(TEntity), typeof(TEntity2), typeof(TEntity3), typeof(TEntity4), typeof(TEntity5), typeof(TEntity6), typeof(TEntity7), typeof(bool));
        }

        private NetSqlQueryable(IDbSet dbSet, QueryBody queryBody) : base(dbSet, queryBody)
        {
        }

        #region ==UseUow==

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> UseUow(IUnitOfWork uow)
        {
            QueryBody.UseUow(uow);
            return this;
        }

        #endregion

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> OrderBy(string colName)
        {
            return Order(new Sort(colName));
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> OrderByDescending(string colName)
        {
            return Order(new Sort(colName, SortType.Desc));
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> OrderBy<TKey>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TKey>> expression)
        {
            QueryBody.SetOrderBy(expression);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> OrderByDescending<TKey>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TKey>> expression)
        {
            QueryBody.SetOrderBy(expression, SortType.Desc);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> Order(Sort sort)
        {
            QueryBody.SetOrderBy(sort);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> Order<TKey>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TKey>> expression, SortType sortType)
        {
            QueryBody.SetOrderBy(expression, sortType);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> Where(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, bool>> expression)
        {
            QueryBody.SetWhere(expression);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> Where(string whereSql)
        {
            QueryBody.SetWhere(whereSql);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> WhereIf(bool condition, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, bool>> expression)
        {
            if (condition)
                Where(expression);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> WhereIf(bool condition, string whereSql)
        {
            if (condition)
                Where(whereSql);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> WhereIf(bool condition, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, bool>> ifExpression, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, bool>> elseExpression)
        {
            Where(condition ? ifExpression : elseExpression);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> WhereIf(bool condition, string ifWhereSql, string elseWhereSql)
        {
            Where(condition ? ifWhereSql : elseWhereSql);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> WhereNotNull(string condition, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, bool>> expression)
        {
            if (condition.NotNull())
                Where(expression);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> WhereNotNull(string condition, string whereSql)
        {
            if (condition.NotNull())
                Where(whereSql);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> WhereNotNull(string condition, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, bool>> ifExpression, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, bool>> elseExpression)
        {
            Where(condition.NotNull() ? ifExpression : elseExpression);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> WhereNotNull(string condition, string ifWhereSql, string elseWhereSql)
        {
            Where(condition.NotNull() ? ifWhereSql : elseWhereSql);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> WhereNotNull(object condition, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, bool>> expression)
        {
            if (condition != null)
                Where(expression);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> WhereNotNull(object condition, string whereSql)
        {
            if (condition != null)
                Where(whereSql);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> WhereNotNull(object condition, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, bool>> ifExpression, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, bool>> elseExpression)
        {
            Where(condition != null ? ifExpression : elseExpression);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> WhereNotNull(object condition, string ifWhereSql, string elseWhereSql)
        {
            Where(condition != null ? ifWhereSql : elseWhereSql);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> WhereNotEmpty(Guid condition, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, bool>> expression)
        {
            if (condition.NotEmpty())
                Where(expression);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> WhereNotEmpty(Guid condition, string whereSql)
        {
            if (condition.NotEmpty())
                Where(whereSql);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> WhereNotEmpty(Guid condition, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, bool>> ifExpression, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, bool>> elseExpression)
        {
            Where(condition.NotEmpty() ? ifExpression : elseExpression);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> WhereNotEmpty(Guid condition, string ifWhereSql, string elseWhereSql)
        {
            Where(condition.NotEmpty() ? ifWhereSql : elseWhereSql);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> WhereNotIn<TKey>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TKey>> key, IEnumerable<TKey> list)
        {
            QueryBody.SetWhereNotIn(key, list);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> Where<TKey>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TKey>> key, QueryOperator queryOperator, INetSqlQueryable queryable)
        {
            QueryBody.SetWhere(key, queryOperator, queryable);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> Limit(int skip, int take)
        {
            QueryBody.SetLimit(skip, take);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> Select<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TResult>> selectExpression)
        {
            QueryBody.SetSelect(selectExpression);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> SelectExclude<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TResult>> expression)
        {
            QueryBody.SetSelectExclude(expression);
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8> LeftJoin<TEntity8>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, bool>> onExpression, string tableName = null, bool noLock = true) where TEntity8 : IEntity, new()
        {
            return new NetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8>(Db, QueryBody, onExpression, JoinType.Left, tableName, noLock);
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8> InnerJoin<TEntity8>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, bool>> onExpression, string tableName = null, bool noLock = true) where TEntity8 : IEntity, new()
        {
            return new NetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8>(Db, QueryBody, onExpression, JoinType.Inner, tableName, noLock);
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8> RightJoin<TEntity8>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, bool>> onExpression, string tableName = null, bool noLock = true) where TEntity8 : IEntity, new()
        {
            return new NetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8>(Db, QueryBody, onExpression, JoinType.Right, tableName, noLock);
        }

        public TResult Max<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TResult>> expression)
        {
            return base.Max<TResult>(expression);
        }

        public Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TResult>> expression)
        {
            return base.MaxAsync<TResult>(expression);
        }

        public TResult Min<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TResult>> expression)
        {
            return base.Min<TResult>(expression);
        }

        public Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TResult>> expression)
        {
            return base.MinAsync<TResult>(expression);
        }

        public TResult Sum<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TResult>> expression)
        {
            return base.Sum<TResult>(expression);
        }

        public Task<TResult> SumAsync<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TResult>> expression)
        {
            return base.SumAsync<TResult>(expression);
        }

        public TResult Avg<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TResult>> expression)
        {
            return base.Avg<TResult>(expression);
        }

        public Task<TResult> AvgAsync<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TResult>> expression)
        {
            return base.AvgAsync<TResult>(expression);
        }

        public IGroupByQueryable7<TResult, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> GroupBy<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TResult>> expression)
        {
            return new GroupByQueryable7<TResult, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>(Db, QueryBody, QueryBuilder, expression);
        }

        public IGroupByQueryable7<INetSqlGroupingKey7<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> GroupBy()
        {
            return new GroupByQueryable7<INetSqlGroupingKey7<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>(Db, QueryBody, QueryBuilder, null);
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

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> IncludeDeleted()
        {
            QueryBody.FilterDeleted = false;
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> NotFilterTenant()
        {
            QueryBody.FilterTenant = false;
            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> Copy()
        {
            return new NetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>(Db, QueryBody.Copy());
        }
    }
}
