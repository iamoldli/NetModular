using System;
using System.Linq.Expressions;
using NetModular.Lib.Data.Abstractions.Entities;

namespace NetModular.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable
{
    /// <summary>
    /// 分组查询
    /// </summary>
    public interface IGroupByQueryable6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> : IGroupByQueryable
        where TEntity : IEntity, new()
        where TEntity2 : IEntity, new()
        where TEntity3 : IEntity, new()
        where TEntity4 : IEntity, new()
        where TEntity5 : IEntity, new()
        where TEntity6 : IEntity, new()
    {
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> Having(Expression<Func<INetSqlGrouping6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6>, bool>> expression);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="customOrderBy">自定义排序规则</param>
        /// <returns></returns>
        IGroupByQueryable6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> OrderBy(string customOrderBy);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="customOrderBy">自定义排序规则</param>
        /// <returns></returns>
        IGroupByQueryable6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> OrderByDescending(string customOrderBy);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> OrderBy<TResult>(Expression<Func<INetSqlGrouping6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6>, TResult>> expression);

        /// <summary>
        /// 倒序排序
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> OrderByDescending<TResult>(Expression<Func<INetSqlGrouping6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6>, TResult>> expression);

        /// <summary>
        /// 查询列
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> Select<TResult>(Expression<Func<INetSqlGrouping6<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6>, TResult>> expression);
    }
}
