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
    internal class GroupByQueryable3<TKey, TEntity, TEntity2, TEntity3> : GroupByQueryableAbstract, IGroupByQueryable3<TKey, TEntity, TEntity2, TEntity3>
        where TEntity : IEntity, new()
        where TEntity2 : IEntity, new()
        where TEntity3 : IEntity, new()
    {
        public GroupByQueryable3(IDbSet db, QueryBody queryBody, QueryBuilder queryBuilder, Expression expression) : base(db, queryBody, queryBuilder, expression)
        {
        }
        public IGroupByQueryable3<TKey, TEntity, TEntity2, TEntity3> Having(Expression<Func<INetSqlGrouping3<TKey, TEntity, TEntity2, TEntity3>, bool>> expression)
        {
            SetHaving(expression);
            return this;
        }

        public IGroupByQueryable3<TKey, TEntity, TEntity2, TEntity3> OrderBy(string customOrderBy)
        {
            SetOrderBy(customOrderBy);
            return this;
        }

        public IGroupByQueryable3<TKey, TEntity, TEntity2, TEntity3> OrderByDescending(string customOrderBy)
        {
            SetOrderByDescending(customOrderBy);
            return this;
        }

        public IGroupByQueryable3<TKey, TEntity, TEntity2, TEntity3> OrderBy<TResult>(Expression<Func<INetSqlGrouping3<TKey, TEntity, TEntity2, TEntity3>, TResult>> expression)
        {
            SetOrderBy(expression);
            return this;
        }

        public IGroupByQueryable3<TKey, TEntity, TEntity2, TEntity3> OrderByDescending<TResult>(Expression<Func<INetSqlGrouping3<TKey, TEntity, TEntity2, TEntity3>, TResult>> expression)
        {
            SetOrderByDescending(expression);
            return this;
        }

        public IGroupByQueryable3<TKey, TEntity, TEntity2, TEntity3> Select<TResult>(Expression<Func<INetSqlGrouping3<TKey, TEntity, TEntity2, TEntity3>, TResult>> expression)
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
