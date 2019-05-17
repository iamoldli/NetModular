using System;
using System.Linq.Expressions;
using Nm.Lib.Data.Abstractions.Entities;

namespace Nm.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable
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
}
