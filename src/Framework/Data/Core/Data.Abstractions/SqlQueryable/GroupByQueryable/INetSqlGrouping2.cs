using System;
using System.Linq.Expressions;
using Nm.Lib.Data.Abstractions.Entities;

namespace Nm.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable
{
    /// <summary>
    /// 分组查询对象
    /// </summary>
    public interface INetSqlGrouping2<out TKey, TEntity, TEntity2> : INetSqlGrouping<TKey> where TEntity : IEntity, new() where TEntity2 : IEntity, new()
    {
        TResult Max<TResult>(Expression<Func<TEntity, TEntity2, TResult>> where);

        TResult Min<TResult>(Expression<Func<TEntity, TEntity2, TResult>> where);

        TResult Sum<TResult>(Expression<Func<TEntity, TEntity2, TResult>> where);

        TResult Avg<TResult>(Expression<Func<TEntity, TEntity2, TResult>> where);
    }
}
