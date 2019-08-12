using System;
using System.Linq.Expressions;
using Tm.Lib.Data.Abstractions.Entities;

namespace Tm.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable
{
    /// <summary>
    /// 分组查询
    /// </summary>
    public interface IGroupByQueryable3<TKey, TEntity, TEntity2, TEntity3> : IGroupByQueryable
        where TEntity : IEntity, new()
        where TEntity2 : IEntity, new()
        where TEntity3 : IEntity, new()
    {
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable3<TKey, TEntity, TEntity2, TEntity3> Having(Expression<Func<INetSqlGrouping3<TKey, TEntity, TEntity2, TEntity3>, bool>> expression);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="customOrderBy">自定义排序规则</param>
        /// <returns></returns>
        IGroupByQueryable3<TKey, TEntity, TEntity2, TEntity3> OrderBy(string customOrderBy);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="customOrderBy">自定义排序规则</param>
        /// <returns></returns>
        IGroupByQueryable3<TKey, TEntity, TEntity2, TEntity3> OrderByDescending(string customOrderBy);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable3<TKey, TEntity, TEntity2, TEntity3> OrderBy<TResult>(Expression<Func<INetSqlGrouping3<TKey, TEntity, TEntity2, TEntity3>, TResult>> expression);

        /// <summary>
        /// 倒序排序
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable3<TKey, TEntity, TEntity2, TEntity3> OrderByDescending<TResult>(Expression<Func<INetSqlGrouping3<TKey, TEntity, TEntity2, TEntity3>, TResult>> expression);

        /// <summary>
        /// 查询列
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable3<TKey, TEntity, TEntity2, TEntity3> Select<TResult>(Expression<Func<INetSqlGrouping3<TKey, TEntity, TEntity2, TEntity3>, TResult>> expression);
    }
}
