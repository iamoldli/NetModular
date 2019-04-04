using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable;
using NetModular.Lib.Data.Core.SqlQueryable.Internal;

namespace NetModular.Lib.Data.Core.SqlQueryable.GroupByQueryable
{
    internal class GroupByQueryable5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5> : GroupByQueryableAbstract, IGroupByQueryable5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5>
        where TEntity : IEntity, new()
        where TEntity2 : IEntity, new()
        where TEntity3 : IEntity, new()
        where TEntity4 : IEntity, new()
        where TEntity5 : IEntity, new()
    {
        public GroupByQueryable5(IDbSet db, QueryBody queryBody, QueryBuilder queryBuilder, Expression expression) : base(db, queryBody, queryBuilder, expression)
        {
        }
        public IGroupByQueryable5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5> Having(Expression<Func<INetSqlGrouping5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5>, bool>> expression)
        {
            SetHaving(expression);
            return this;
        }

        public IGroupByQueryable5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5> OrderBy(string customOrderBy)
        {
            SetOrderBy(customOrderBy);
            return this;
        }

        public IGroupByQueryable5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5> OrderByDescending(string customOrderBy)
        {
            SetOrderByDescending(customOrderBy);
            return this;
        }

        public IGroupByQueryable5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5> OrderBy<TResult>(Expression<Func<INetSqlGrouping5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5>, TResult>> expression)
        {
            SetOrderBy(expression);
            return this;
        }

        public IGroupByQueryable5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5> OrderByDescending<TResult>(Expression<Func<INetSqlGrouping5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5>, TResult>> expression)
        {
            SetOrderByDescending(expression);
            return this;
        }

        public IGroupByQueryable5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5> Select<TResult>(Expression<Func<INetSqlGrouping5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5>, TResult>> expression)
        {
            SetSelect(expression);
            return this;
        }

        public IList<TEntity> ToList()
        {
            return ToList<TEntity>();
        }

        public Task<IList<TEntity>> ToListAsync()
        {
            return ToListAsync<TEntity>();
        }
    }
}
