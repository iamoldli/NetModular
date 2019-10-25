using System;
using System.Linq.Expressions;
using NetModular.Lib.Data.Abstractions.Entities;

namespace NetModular.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable
{
    /// <summary>
    /// 分组查询
    /// </summary>
    public interface IGroupByQueryable2<TKey, TEntity, TEntity2> : IGroupByQueryable where TEntity : IEntity, new() where TEntity2 : IEntity, new()
    {
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable2<TKey, TEntity, TEntity2> Having(Expression<Func<INetSqlGrouping2<TKey, TEntity, TEntity2>, bool>> expression);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="customOrderBy">自定义排序规则</param>
        /// <returns></returns>
        IGroupByQueryable2<TKey, TEntity, TEntity2> OrderBy(string customOrderBy);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="customOrderBy">自定义排序规则</param>
        /// <returns></returns>
        IGroupByQueryable2<TKey, TEntity, TEntity2> OrderByDescending(string customOrderBy);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable2<TKey, TEntity, TEntity2> OrderBy<TResult>(Expression<Func<INetSqlGrouping2<TKey, TEntity, TEntity2>, TResult>> expression);

        /// <summary>
        /// 倒序排序
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable2<TKey, TEntity, TEntity2> OrderByDescending<TResult>(Expression<Func<INetSqlGrouping2<TKey, TEntity, TEntity2>, TResult>> expression);

        /// <summary>
        /// 查询列
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable2<TKey, TEntity, TEntity2> Select<TResult>(Expression<Func<INetSqlGrouping2<TKey, TEntity, TEntity2>, TResult>> expression);
    }
}
