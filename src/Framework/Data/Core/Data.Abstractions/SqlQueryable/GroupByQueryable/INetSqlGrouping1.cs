using System;
using System.Linq.Expressions;
using NetModular.Lib.Data.Abstractions.Entities;

namespace NetModular.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable
{
    /// <summary>
    /// 分组查询对象
    /// </summary>
    public interface INetSqlGrouping1<out TKey, TEntity> : INetSqlGrouping<TKey> where TEntity : IEntity
    {
        TResult Max<TResult>(Expression<Func<TEntity, TResult>> where);

        TResult Min<TResult>(Expression<Func<TEntity, TResult>> where);

        TResult Sum<TResult>(Expression<Func<TEntity, TResult>> where);

        TResult Avg<TResult>(Expression<Func<TEntity, TResult>> where);
    }

    /// <summary>
    /// 分组查询对象Key
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface INetSqlGroupingKey1<out TEntity>
    {
        TEntity T1 { get; }
    }
}
