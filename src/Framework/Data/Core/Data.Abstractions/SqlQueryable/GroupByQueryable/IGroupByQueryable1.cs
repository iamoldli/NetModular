using System;
using System.Linq.Expressions;
using NetModular.Lib.Data.Abstractions.Entities;

namespace NetModular.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable
{
    /// <summary>
    /// 分组查询
    /// </summary>
    public interface IGroupByQueryable1<TKey, TEntity> : IGroupByQueryable where TEntity : IEntity
    {
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable1<TKey, TEntity> Having(Expression<Func<INetSqlGrouping1<TKey, TEntity>, bool>> expression);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="customOrderBy">自定义排序规则</param>
        /// <returns></returns>
        IGroupByQueryable1<TKey, TEntity> OrderBy(string customOrderBy);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="customOrderBy">自定义排序规则</param>
        /// <returns></returns>
        IGroupByQueryable1<TKey, TEntity> OrderByDescending(string customOrderBy);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable1<TKey, TEntity> OrderBy<TResult>(Expression<Func<INetSqlGrouping1<TKey, TEntity>, TResult>> expression);

        /// <summary>
        /// 倒序排序
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable1<TKey, TEntity> OrderByDescending<TResult>(Expression<Func<INetSqlGrouping1<TKey, TEntity>, TResult>> expression);

        /// <summary>
        /// 查询列
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable1<TKey, TEntity> Select<TResult>(Expression<Func<INetSqlGrouping1<TKey, TEntity>, TResult>> expression);
    }
}
