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
    internal class GroupByQueryable2<TKey, TEntity, TEntity2> : GroupByQueryableAbstract, IGroupByQueryable2<TKey, TEntity, TEntity2> where TEntity : IEntity, new() where TEntity2 : IEntity, new()
    {
        public GroupByQueryable2(IDbSet db, QueryBody queryBody, QueryBuilder queryBuilder, Expression expression) : base(db, queryBody, queryBuilder, expression)
        {
        }
        public IGroupByQueryable2<TKey, TEntity, TEntity2> Having(Expression<Func<INetSqlGrouping2<TKey, TEntity, TEntity2>, bool>> expression)
        {
            SetHaving(expression);
            return this;
        }

        public IGroupByQueryable2<TKey, TEntity, TEntity2> OrderBy(string customOrderBy)
        {
            SetOrderBy(customOrderBy);
            return this;
        }

        public IGroupByQueryable2<TKey, TEntity, TEntity2> OrderByDescending(string customOrderBy)
        {
            SetOrderByDescending(customOrderBy);
            return this;
        }

        public IGroupByQueryable2<TKey, TEntity, TEntity2> OrderBy<TResult>(Expression<Func<INetSqlGrouping2<TKey, TEntity, TEntity2>, TResult>> expression)
        {
            SetOrderBy(expression);
            return this;
        }

        public IGroupByQueryable2<TKey, TEntity, TEntity2> OrderByDescending<TResult>(Expression<Func<INetSqlGrouping2<TKey, TEntity, TEntity2>, TResult>> expression)
        {
            SetOrderByDescending(expression);
            return this;
        }

        public IGroupByQueryable2<TKey, TEntity, TEntity2> Select<TResult>(Expression<Func<INetSqlGrouping2<TKey, TEntity, TEntity2>, TResult>> expression)
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
