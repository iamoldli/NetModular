using System;
using System.Linq.Expressions;
using Nm.Lib.Data.Abstractions.Entities;

namespace Nm.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable
{
    /// <summary>
    /// 分组查询对象
    /// </summary>
    public interface INetSqlGrouping9<out TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9> : INetSqlGrouping<TKey>
        where TEntity : IEntity, new()
        where TEntity2 : IEntity, new()
        where TEntity3 : IEntity, new()
        where TEntity4 : IEntity, new()
        where TEntity5 : IEntity, new()
        where TEntity6 : IEntity, new()
        where TEntity7 : IEntity, new()
        where TEntity8 : IEntity, new()
        where TEntity9 : IEntity, new()
    {
        TResult Max<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TResult>> where);

        TResult Min<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TResult>> where);

        TResult Sum<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TResult>> where);

        TResult Avg<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TResult>> where);
    }
}
