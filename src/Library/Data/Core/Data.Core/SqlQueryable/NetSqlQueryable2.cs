using System;
using System.Collections.Generic;
using System.Data;
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
using Nm.Lib.Utils.Core;

namespace Nm.Lib.Data.Core.SqlQueryable
{
    internal class NetSqlQueryable<TEntity, TEntity2> : NetSqlQueryableAbstract, INetSqlQueryable<TEntity, TEntity2>
        where TEntity : IEntity, new()
        where TEntity2 : IEntity, new()
    {
        public NetSqlQueryable(IDbSet dbSet, QueryBody queryBody, Expression<Func<TEntity, TEntity2, bool>> onExpression, JoinType joinType = JoinType.Left)
            : base(dbSet, queryBody)
        {
            Check.NotNull(onExpression, nameof(onExpression), "请输入连接条件");

            QueryBody.JoinDescriptors.Add(new QueryJoinDescriptor
            {
                Type = joinType,
                Alias = "T2",
                EntityDescriptor = EntityDescriptorCollection.Get<TEntity2>(),
                On = onExpression
            });

            QueryBody.WhereDelegateType = typeof(Func<,,>).MakeGenericType(typeof(TEntity), typeof(TEntity2), typeof(bool));
        }

        #region ==UseTran==

        public INetSqlQueryable<TEntity, TEntity2> UseTran(IDbTransaction transaction)
        {
            QueryBody.UseTran(transaction);
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

        public INetSqlQueryable<TEntity, TEntity2> WhereIf(bool condition, Expression<Func<TEntity, TEntity2, bool>> expression)
        {
            if (condition)
                Where(expression);

            return this;
        }

        public INetSqlQueryable<TEntity, TEntity2> WhereIf(bool condition, Expression<Func<TEntity, TEntity2, bool>> ifExpression, Expression<Func<TEntity, TEntity2, bool>> elseExpression)
        {
            Where(condition ? ifExpression : elseExpression);

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

        public INetSqlQueryable<TEntity, TEntity2, TEntity3> LeftJoin<TEntity3>(Expression<Func<TEntity, TEntity2, TEntity3, bool>> onExpression) where TEntity3 : IEntity, new()
        {
            return new NetSqlQueryable<TEntity, TEntity2, TEntity3>(Db, QueryBody, onExpression);
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3> InnerJoin<TEntity3>(Expression<Func<TEntity, TEntity2, TEntity3, bool>> onExpression) where TEntity3 : IEntity, new()
        {
            return new NetSqlQueryable<TEntity, TEntity2, TEntity3>(Db, QueryBody, onExpression, JoinType.Inner);
        }

        public INetSqlQueryable<TEntity, TEntity2, TEntity3> RightJoin<TEntity3>(Expression<Func<TEntity, TEntity2, TEntity3, bool>> onExpression) where TEntity3 : IEntity, new()
        {
            return new NetSqlQueryable<TEntity, TEntity2, TEntity3>(Db, QueryBody, onExpression, JoinType.Right);
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

        public IGroupByQueryable2<TResult, TEntity, TEntity2> GroupBy<TResult>(Expression<Func<TEntity, TEntity2, TResult>> expression)
        {
            return new GroupByQueryable2<TResult, TEntity, TEntity2>(Db, QueryBody, QueryBuilder, expression);
        }
    }
}
