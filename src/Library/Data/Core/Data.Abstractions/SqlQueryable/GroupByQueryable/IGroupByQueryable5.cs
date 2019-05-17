using System;
using System.Linq.Expressions;
using Nm.Lib.Data.Abstractions.Entities;

namespace Nm.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable
{
    /// <summary>
    /// 分组查询
    /// </summary>
    public interface IGroupByQueryable5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5> : IGroupByQueryable
        where TEntity : IEntity, new()
        where TEntity2 : IEntity, new()
        where TEntity3 : IEntity, new()
        where TEntity4 : IEntity, new()
        where TEntity5 : IEntity, new()
    {
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5> Having(Expression<Func<INetSqlGrouping5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5>, bool>> expression);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="customOrderBy">自定义排序规则</param>
        /// <returns></returns>
        IGroupByQueryable5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5> OrderBy(string customOrderBy);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="customOrderBy">自定义排序规则</param>
        /// <returns></returns>
        IGroupByQueryable5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5> OrderByDescending(string customOrderBy);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5> OrderBy<TResult>(Expression<Func<INetSqlGrouping5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5>, TResult>> expression);

        /// <summary>
        /// 倒序排序
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5> OrderByDescending<TResult>(Expression<Func<INetSqlGrouping5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5>, TResult>> expression);

        /// <summary>
        /// 查询列
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        IGroupByQueryable5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5> Select<TResult>(Expression<Func<INetSqlGrouping5<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5>, TResult>> expression);
    }
}
