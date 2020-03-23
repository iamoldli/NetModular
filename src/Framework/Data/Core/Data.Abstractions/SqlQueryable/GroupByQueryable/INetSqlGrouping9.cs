using System;
using System.Linq.Expressions;
using NetModular.Lib.Data.Abstractions.Entities;

namespace NetModular.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable
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

    /// <summary>
    /// 分组查询对象Key
    /// </summary>
    public interface INetSqlGroupingKey9<out TEntity, out TEntity2, out TEntity3, out TEntity4, out TEntity5, out TEntity6, out TEntity7, out TEntity8, out TEntity9>
    {
        TEntity T1 { get; }

        TEntity2 T2 { get; }

        TEntity3 T3 { get; }

        TEntity4 T4 { get; }

        TEntity5 T5 { get; }

        TEntity6 T6 { get; }

        TEntity7 T7 { get; }

        TEntity8 T8 { get; }

        TEntity9 T9 { get; }
    }
}
