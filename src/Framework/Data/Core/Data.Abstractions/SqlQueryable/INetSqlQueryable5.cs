using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Abstractions.Enums;
using NetModular.Lib.Data.Abstractions.Pagination;
using NetModular.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable;

namespace NetModular.Lib.Data.Abstractions.SqlQueryable
{
    public interface INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> : INetSqlQueryable
        where TEntity : IEntity, new()
        where TEntity2 : IEntity, new()
        where TEntity3 : IEntity, new()
        where TEntity4 : IEntity, new()
        where TEntity5 : IEntity, new()
    {
        #region ==使用工作单元==

        /// <summary>
        /// 使用工作单元
        /// </summary>
        /// <param name="uow"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> UseUow(IUnitOfWork uow);

        #endregion

        #region ==Sort==

        /// <summary>
        /// 升序
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> OrderBy(string colName);

        /// <summary>
        /// 降序
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> OrderByDescending(string colName);

        /// <summary>
        /// 升序
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> OrderBy<TKey>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TKey>> expression);

        /// <summary>
        /// 降序
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> OrderByDescending<TKey>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TKey>> expression);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> Order(Sort sort);

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="expression"></param>
        /// <param name="sortType"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> Order<TKey>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TKey>> expression, SortType sortType);

        #endregion

        #region ==Where==

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="expression">过滤条件</param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> Where(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, bool>> expression);

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="whereSql">过滤条件</param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> Where(string whereSql);

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="condition">添加条件</param>
        /// <param name="expression">条件</param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> WhereIf(bool condition, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, bool>> expression);

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="condition">添加条件</param>
        /// <param name="whereSql">条件</param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> WhereIf(bool condition, string whereSql);

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ifExpression"></param>
        /// <param name="elseExpression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> WhereIf(bool condition, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, bool>> ifExpression, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, bool>> elseExpression);

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ifWhereSql"></param>
        /// <param name="elseWhereSql"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> WhereIf(bool condition, string ifWhereSql, string elseWhereSql);

        /// <summary>
        /// 字符串不为Null以及空字符串的时候添加过滤
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> WhereNotNull(string condition, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, bool>> expression);

        /// <summary>
        /// 字符串不为Null以及空字符串的时候添加过滤
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> WhereNotNull(string condition, string whereSql);

        /// <summary>
        /// 字符串不为Null以及空字符串的时候添加ifExpression，反之添加elseExpression
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ifExpression"></param>
        /// <param name="elseExpression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> WhereNotNull(string condition, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, bool>> ifExpression, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, bool>> elseExpression);

        /// <summary>
        /// 字符串不为Null以及空字符串的时候添加ifExpression，反之添加elseExpression
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ifWhereSql"></param>
        /// <param name="elseWhereSql"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> WhereNotNull(string condition, string ifWhereSql, string elseWhereSql);

        /// <summary>
        /// 不为Null以及Empty的时候添加过滤
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> WhereNotNull(object condition, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, bool>> expression);

        /// <summary>
        /// 不为Null以及Empty的时候添加过滤
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> WhereNotNull(object condition, string whereSql);

        /// <summary>
        /// 不为Null以及Empty的时候添加ifExpression，反之添加elseExpression
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ifExpression"></param>
        /// <param name="elseExpression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> WhereNotNull(object condition, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, bool>> ifExpression, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, bool>> elseExpression);

        /// <summary>
        /// 不为Null以及Empty的时候添加ifExpression，反之添加elseExpression
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ifWhereSql"></param>
        /// <param name="elseWhereSql"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> WhereNotNull(object condition, string ifWhereSql, string elseWhereSql);

        /// <summary>
        /// GUID不为空的时候添加过滤条件
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> WhereNotEmpty(Guid condition, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, bool>> expression);

        /// <summary>
        /// GUID不为空的时候添加过滤条件
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> WhereNotEmpty(Guid condition, string whereSql);

        /// <summary>
        /// GUID不为空的时候添加ifExpression，反之添加elseExpression
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ifExpression"></param>
        /// <param name="elseExpression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> WhereNotEmpty(Guid condition, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, bool>> ifExpression, Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, bool>> elseExpression);

        /// <summary>
        /// GUID不为空的时候添加ifExpression，反之添加elseExpression
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ifWhereSql"></param>
        /// <param name="elseWhereSql"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> WhereNotEmpty(Guid condition, string ifWhereSql, string elseWhereSql);

        /// <summary>
        /// NotIn查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> WhereNotIn<TKey>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TKey>> key, IEnumerable<TKey> list);

        /// <summary>
        /// 子查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key">列</param>
        /// <param name="queryOperator">运算逻辑</param>
        /// <param name="queryable">子查询的查询构造器</param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> Where<TKey>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TKey>> key, QueryOperator queryOperator, INetSqlQueryable queryable);

        #endregion

        #region ==Limit==

        /// <summary>
        /// 限制
        /// </summary>
        /// <param name="skip">跳过前几条数据</param>
        /// <param name="take">取前几条数据</param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> Limit(int skip, int take);

        #endregion

        #region ==Select==

        /// <summary>
        /// 查询指定列
        /// </summary>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> Select<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TResult>> selectExpression);

        /// <summary>
        /// 查询排除指定列
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> SelectExclude<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TResult>> expression);

        #endregion

        #region ==Join==

        /// <summary>
        /// 左连接
        /// </summary>
        /// <typeparam name="TEntity6"></typeparam>
        /// <param name="onExpression"></param>
        /// <param name="tableName"></param>
        /// <param name="noLock">针对SqlServer的NoLock特性，默认开启</param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> LeftJoin<TEntity6>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, bool>> onExpression, string tableName = null, bool noLock = true) where TEntity6 : IEntity, new();

        /// <summary>
        /// 内连接
        /// </summary>
        /// <typeparam name="TEntity6"></typeparam>
        /// <param name="onExpression"></param>
        /// <param name="tableName"></param>
        /// <param name="noLock">针对SqlServer的NoLock特性，默认开启</param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> InnerJoin<TEntity6>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, bool>> onExpression, string tableName = null, bool noLock = true) where TEntity6 : IEntity, new();

        /// <summary>
        /// 右连接
        /// </summary>
        /// <typeparam name="TEntity6"></typeparam>
        /// <param name="onExpression"></param>
        /// <param name="tableName"></param>
        /// <param name="noLock">针对SqlServer的NoLock特性，默认开启</param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6> RightJoin<TEntity6>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, bool>> onExpression, string tableName = null, bool noLock = true) where TEntity6 : IEntity, new();

        #endregion

        #region ==Max==

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        TResult Max<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TResult>> expression);

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TResult>> expression);

        #endregion

        #region ==Min==

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        TResult Min<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TResult>> expression);

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TResult>> expression);

        #endregion

        #region ==Sum==

        /// <summary>
        /// 求和
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        TResult Sum<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TResult>> expression);

        /// <summary>
        /// 求和
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TResult> SumAsync<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TResult>> expression);

        #endregion

        #region ==Avg==

        /// <summary>
        /// 求平均值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        TResult Avg<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TResult>> expression);

        /// <summary>
        /// 求平均值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TResult> AvgAsync<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TResult>> expression);

        #endregion

        #region ==GroupBy==

        /// <summary>
        /// 分组
        /// </summary>
        /// <returns></returns>
        IGroupByQueryable5<TResult, TEntity, TEntity2, TEntity3, TEntity4, TEntity5> GroupBy<TResult>(Expression<Func<TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TResult>> expression);

        /// <summary>
        /// 分组(group by 1)
        /// </summary>
        /// <returns></returns>
        IGroupByQueryable5<INetSqlGroupingKey5<TEntity, TEntity2, TEntity3, TEntity4, TEntity5>, TEntity, TEntity2, TEntity3, TEntity4, TEntity5> GroupBy();

        #endregion

        #region ==ToList==

        /// <summary>
        /// 查询列表，返回指定类型
        /// </summary>
        /// <returns></returns>
        new IList<TEntity> ToList();

        /// <summary>
        /// 查询列表，返回指定类型
        /// </summary>
        /// <returns></returns>
        new Task<IList<TEntity>> ToListAsync();

        #endregion

        #region ==Pagination==

        /// <summary>
        /// 分页查询，返回实体类型
        /// </summary>
        /// <param name="paging">分页对象</param>
        /// <returns></returns>
        new IList<TEntity> Pagination(Paging paging = null);

        /// <summary>
        /// 分页查询，返回实体类型
        /// </summary>
        /// <param name="paging">分页对象</param>
        /// <returns></returns>
        new Task<IList<TEntity>> PaginationAsync(Paging paging = null);

        #endregion

        #region ==First==

        /// <summary>
        /// 查询第一条数据，返回指定类型
        /// </summary>
        /// <returns></returns>
        new TEntity First();

        /// <summary>
        /// 查询第一条数据，返回指定类型
        /// </summary>
        /// <returns></returns>
        new Task<TEntity> FirstAsync();

        #endregion

        #region ==IncludeDeleted==

        /// <summary>
        /// 包含已删除的数据
        /// </summary>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> IncludeDeleted();

        #endregion

        #region ==NotFilterTenant==

        /// <summary>
        /// 不过滤租户
        /// </summary>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> NotFilterTenant();

        #endregion

        #region ==Copy==

        /// <summary>
        /// 复制一个新的实例
        /// </summary>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2, TEntity3, TEntity4, TEntity5> Copy();

        #endregion
    }
}
