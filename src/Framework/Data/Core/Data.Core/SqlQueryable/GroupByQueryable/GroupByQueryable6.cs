using System;
using System.Linq.Expressions;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable;
using NetModular.Lib.Data.Core.SqlQueryable.Internal;

namespace NetModular.Lib.Data.Core.SqlQueryable.GroupByQueryable
{
    internal class GroupByQueryable6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> : GroupByQueryableAbstract, IGroupByQueryable6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6>
        where TEntity : IEntity, new()
        where TEntity2 : IEntity, new()
        where TEntity3 : IEntity, new()
        where TEntity4 : IEntity, new()
        where TEntity5 : IEntity, new()
        where TEntity6 : IEntity, new()
    {
        public GroupByQueryable6(IDbSet db, QueryBody queryBody, QueryBuilder queryBuilder, Expression expression) : base(db, queryBody, queryBuilder, expression)
        {
        }
        public IGroupByQueryable6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> Having(Expression<Func<INetSqlGrouping6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6>, bool>> expression)
        {
            SetHaving(expression);
            return this;
        }

        public IGroupByQueryable6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> OrderBy(string customOrderBy)
        {
            SetOrderBy(customOrderBy);
            return this;
        }

        public IGroupByQueryable6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> OrderByDescending(string customOrderBy)
        {
            SetOrderByDescending(customOrderBy);
            return this;
        }

        public IGroupByQueryable6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> OrderBy<TResult>(Expression<Func<INetSqlGrouping6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6>, TResult>> expression)
        {
            SetOrderBy(expression);
            return this;
        }

        public IGroupByQueryable6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> OrderByDescending<TResult>(Expression<Func<INetSqlGrouping6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6>, TResult>> expression)
        {
            SetOrderByDescending(expression);
            return this;
        }

        public IGroupByQueryable6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> Select<TResult>(Expression<Func<INetSqlGrouping6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6>, TResult>> expression)
        {
            SetSelect(expression);
            return this;
        }
    }
}
