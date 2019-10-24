using System;
using System.Linq.Expressions;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Entities;
using Nm.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable;
using Nm.Lib.Data.Core.SqlQueryable.Internal;

namespace Nm.Lib.Data.Core.SqlQueryable.GroupByQueryable
{
    internal class GroupByQueryable4<TKey, TEntity, TEntity2, TEntity3, TEntity4> : GroupByQueryableAbstract, IGroupByQueryable4<TKey, TEntity, TEntity2, TEntity3, TEntity4>
        where TEntity : IEntity, new()
        where TEntity2 : IEntity, new()
        where TEntity3 : IEntity, new()
        where TEntity4 : IEntity, new()
    {
        public GroupByQueryable4(IDbSet db, QueryBody queryBody, QueryBuilder queryBuilder, Expression expression) : base(db, queryBody, queryBuilder, expression)
        {
        }
        public IGroupByQueryable4<TKey, TEntity, TEntity2, TEntity3, TEntity4> Having(Expression<Func<INetSqlGrouping4<TKey, TEntity, TEntity2, TEntity3, TEntity4>, bool>> expression)
        {
            SetHaving(expression);
            return this;
        }

        public IGroupByQueryable4<TKey, TEntity, TEntity2, TEntity3, TEntity4> OrderBy(string customOrderBy)
        {
            SetOrderBy(customOrderBy);
            return this;
        }

        public IGroupByQueryable4<TKey, TEntity, TEntity2, TEntity3, TEntity4> OrderByDescending(string customOrderBy)
        {
            SetOrderByDescending(customOrderBy);
            return this;
        }

        public IGroupByQueryable4<TKey, TEntity, TEntity2, TEntity3, TEntity4> OrderBy<TResult>(Expression<Func<INetSqlGrouping4<TKey, TEntity, TEntity2, TEntity3, TEntity4>, TResult>> expression)
        {
            SetOrderBy(expression);
            return this;
        }

        public IGroupByQueryable4<TKey, TEntity, TEntity2, TEntity3, TEntity4> OrderByDescending<TResult>(Expression<Func<INetSqlGrouping4<TKey, TEntity, TEntity2, TEntity3, TEntity4>, TResult>> expression)
        {
            SetOrderByDescending(expression);
            return this;
        }

        public IGroupByQueryable4<TKey, TEntity, TEntity2, TEntity3, TEntity4> Select<TResult>(Expression<Func<INetSqlGrouping4<TKey, TEntity, TEntity2, TEntity3, TEntity4>, TResult>> expression)
        {
            SetSelect(expression);
            return this;
        }
    }
}
