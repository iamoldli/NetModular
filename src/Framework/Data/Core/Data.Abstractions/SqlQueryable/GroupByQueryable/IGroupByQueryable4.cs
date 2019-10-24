using System;
using System.Linq.Expressions;
using Nm.Lib.Data.Abstractions.Entities;

namespace Nm.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable
{
    /// <summary>
    /// 分组查询
    /// </summary>
    public interface IGroupByQueryable4<TKey, TEntity, TEntity2, TEntity3, TEntity4> : IGroupByQueryable
        where TEntity : IEntity, new()
        where TEntity2 : IEntity, new()
        where TEntity3 : IEntity, new()
        where TEntity4 : IEntity, new()
    {
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable4<TKey, TEntity, TEntity2, TEntity3, TEntity4> Having(Expression<Func<INetSqlGrouping4<TKey, TEntity, TEntity2, TEntity3, TEntity4>, bool>> expression);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="customOrderBy">自定义排序规则</param>
        /// <returns></returns>
        IGroupByQueryable4<TKey, TEntity, TEntity2, TEntity3, TEntity4> OrderBy(string customOrderBy);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="customOrderBy">自定义排序规则</param>
        /// <returns></returns>
        IGroupByQueryable4<TKey, TEntity, TEntity2, TEntity3, TEntity4> OrderByDescending(string customOrderBy);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable4<TKey, TEntity, TEntity2, TEntity3, TEntity4> OrderBy<TResult>(Expression<Func<INetSqlGrouping4<TKey, TEntity, TEntity2, TEntity3, TEntity4>, TResult>> expression);

        /// <summary>
        /// 倒序排序
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable4<TKey, TEntity, TEntity2, TEntity3, TEntity4> OrderByDescending<TResult>(Expression<Func<INetSqlGrouping4<TKey, TEntity, TEntity2, TEntity3, TEntity4>, TResult>> expression);

        /// <summary>
        /// 查询列
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable4<TKey, TEntity, TEntity2, TEntity3, TEntity4> Select<TResult>(Expression<Func<INetSqlGrouping4<TKey, TEntity, TEntity2, TEntity3, TEntity4>, TResult>> expression);
    }
}
