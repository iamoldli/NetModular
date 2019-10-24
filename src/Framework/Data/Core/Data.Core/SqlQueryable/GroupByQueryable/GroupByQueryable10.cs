using System;
using System.Linq.Expressions;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Entities;
using Nm.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable;
using Nm.Lib.Data.Core.SqlQueryable.Internal;

namespace Nm.Lib.Data.Core.SqlQueryable.GroupByQueryable
{
    internal class GroupByQueryable10<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10> : GroupByQueryableAbstract, IGroupByQueryable10<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10>
        where TEntity : IEntity, new()
        where TEntity2 : IEntity, new()
        where TEntity3 : IEntity, new()
        where TEntity4 : IEntity, new()
        where TEntity5 : IEntity, new()
        where TEntity6 : IEntity, new()
        where TEntity7 : IEntity, new()
        where TEntity8 : IEntity, new()
        where TEntity9 : IEntity, new()
        where TEntity10 : IEntity, new()
    {
        public GroupByQueryable10(IDbSet db, QueryBody queryBody, QueryBuilder queryBuilder, Expression expression) : base(db, queryBody, queryBuilder, expression)
        {
        }
        public IGroupByQueryable10<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10> Having(Expression<Func<INetSqlGrouping10<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10>, bool>> expression)
        {
            SetHaving(expression);
            return this;
        }

        public IGroupByQueryable10<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10> OrderBy(string customOrderBy)
        {
            SetOrderBy(customOrderBy);
            return this;
        }

        public IGroupByQueryable10<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10> OrderByDescending(string customOrderBy)
        {
            SetOrderByDescending(customOrderBy);
            return this;
        }

        public IGroupByQueryable10<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10> OrderBy<TResult>(Expression<Func<INetSqlGrouping10<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10>, TResult>> expression)
        {
            SetOrderBy(expression);
            return this;
        }

        public IGroupByQueryable10<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10> OrderByDescending<TResult>(Expression<Func<INetSqlGrouping10<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10>, TResult>> expression)
        {
            SetOrderByDescending(expression);
            return this;
        }

        public IGroupByQueryable10<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10> Select<TResult>(Expression<Func<INetSqlGrouping10<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10>, TResult>> expression)
        {
            SetSelect(expression);
            return this;
        }
    }
}
