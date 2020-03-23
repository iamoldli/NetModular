using System;
using System.Linq.Expressions;
using NetModular.Lib.Data.Abstractions.Entities;

namespace NetModular.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable
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

    /// <summary>
    /// 分组查询对象Key
    /// </summary>
    public interface INetSqlGroupingKey2<out TEntity, out TEntity2>
    {
        TEntity T1 { get; }

        TEntity2 T2 { get; }
    }
}
