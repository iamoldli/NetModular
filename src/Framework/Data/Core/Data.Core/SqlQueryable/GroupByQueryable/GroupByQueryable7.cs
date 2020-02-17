using System;
using System.Linq.Expressions;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable;
using NetModular.Lib.Data.Core.SqlQueryable.Internal;

namespace NetModular.Lib.Data.Core.SqlQueryable.GroupByQueryable
{
    internal class GroupByQueryable7<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> : GroupByQueryableAbstract, IGroupByQueryable7<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>
        where TEntity : IEntity, new()
        where TEntity2 : IEntity, new()
        where TEntity3 : IEntity, new()
        where TEntity4 : IEntity, new()
        where TEntity5 : IEntity, new()
        where TEntity6 : IEntity, new()
        where TEntity7 : IEntity, new()
    {
        public GroupByQueryable7(IDbSet db, QueryBody queryBody, QueryBuilder queryBuilder, Expression expression) : base(db, queryBody, queryBuilder, expression)
        {
        }
        public IGroupByQueryable7<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> Having(Expression<Func<INetSqlGrouping7<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>, bool>> expression)
        {
            SetHaving(expression);
            return this;
        }

        public IGroupByQueryable7<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> OrderBy(string customOrderBy)
        {
            SetOrderBy(customOrderBy);
            return this;
        }

        public IGroupByQueryable7<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> OrderByDescending(string customOrderBy)
        {
            SetOrderByDescending(customOrderBy);
            return this;
        }

        public IGroupByQueryable7<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> OrderBy<TResult>(Expression<Func<INetSqlGrouping7<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>, TResult>> expression)
        {
            SetOrderBy(expression);
            return this;
        }

        public IGroupByQueryable7<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> OrderByDescending<TResult>(Expression<Func<INetSqlGrouping7<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>, TResult>> expression)
        {
            SetOrderByDescending(expression);
            return this;
        }

        public IGroupByQueryable7<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7> Select<TResult>(Expression<Func<INetSqlGrouping7<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>, TResult>> expression)
        {
            SetSelect(expression);
            return this;
        }
    }
}
