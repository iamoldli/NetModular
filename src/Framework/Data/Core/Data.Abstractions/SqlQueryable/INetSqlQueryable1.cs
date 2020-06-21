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
    /// <summary>
    /// Sql构造器
    /// </summary>
    public interface INetSqlQueryable<TEntity> : INetSqlQueryable where TEntity : IEntity, new()
    {
        #region ==使用工作单元==

        /// <summary>
        /// 使用工作单元
        /// </summary>
        /// <param name="uow"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> UseUow(IUnitOfWork uow);

        #endregion

        #region ==Sort==

        /// <summary>
        /// 升序
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> OrderBy(string colName);

        /// <summary>
        /// 降序
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> OrderByDescending(string colName);

        /// <summary>
        /// 升序
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> expression);

        /// <summary>
        /// 降序
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> expression);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> Order(Sort sort);

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="expression"></param>
        /// <param name="sortType"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> Order<TKey>(Expression<Func<TEntity, TKey>> expression, SortType sortType);

        #endregion

        #region ==Where==

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="expression">过滤条件</param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 附加SQL语句条件
        /// </summary>
        /// <param name="whereSql">查询条件</param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> Where(string whereSql);

        /// <summary>
        /// 条件为true时添加过滤
        /// </summary>
        /// <param name="condition">添加条件</param>
        /// <param name="expression">条件</param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> WhereIf(bool condition, Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 条件为true时添加SQL语句条件
        /// </summary>
        /// <param name="condition">添加条件</param>
        /// <param name="whereSql">查询条件</param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> WhereIf(bool condition, string whereSql);

        /// <summary>
        /// 根据条件添加过滤
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ifExpression"></param>
        /// <param name="elseExpression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> WhereIf(bool condition, Expression<Func<TEntity, bool>> ifExpression, Expression<Func<TEntity, bool>> elseExpression);

        /// <summary>
        /// 根据条件添加SQL语句条件
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ifWhereSql"></param>
        /// <param name="elseWhereSql"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> WhereIf(bool condition, string ifWhereSql, string elseWhereSql);

        /// <summary>
        /// 字符串不为Null以及空字符串的时候添加过滤
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> WhereNotNull(string condition, Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 字符串不为Null以及空字符串的时候添加SQL语句条件
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> WhereNotNull(string condition, string whereSql);

        /// <summary>
        /// 字符串不为Null以及空字符串的时候添加ifExpression，反之添加elseExpression
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ifExpression"></param>
        /// <param name="elseExpression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> WhereNotNull(string condition, Expression<Func<TEntity, bool>> ifExpression, Expression<Func<TEntity, bool>> elseExpression);

        /// <summary>
        /// 字符串不为Null以及空字符串的时候添加ifWhereSql，反之添加elseWhereSql
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ifWhereSql"></param>
        /// <param name="elseWhereSql"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> WhereNotNull(string condition, string ifWhereSql, string elseWhereSql);

        /// <summary>
        /// 对象不为Null以及空字符串的时候添加SQL语句条件
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> WhereNotNull(object condition, Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 对象不为Null以及空字符串的时候添加SQL语句条件
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> WhereNotNull(object condition, string whereSql);

        /// <summary>
        /// 对象不为Null以及空字符串的时候添加ifExpression，反之添加elseExpression
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ifExpression"></param>
        /// <param name="elseExpression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> WhereNotNull(object condition, Expression<Func<TEntity, bool>> ifExpression, Expression<Func<TEntity, bool>> elseExpression);

        /// <summary>
        /// 对象不为Null以及空字符串的时候添加ifWhereSql，反之添加elseWhereSql
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ifWhereSql"></param>
        /// <param name="elseWhereSql"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> WhereNotNull(object condition, string ifWhereSql, string elseWhereSql);

        /// <summary>
        /// GUID不为空的时候添加过滤条件
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> WhereNotEmpty(Guid condition, Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// GUID不为空的时候添加过滤SQL语句条件
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> WhereNotEmpty(Guid condition, string whereSql);

        /// <summary>
        /// GUID不为空的时候添加ifExpression，反之添加elseExpression
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ifExpression"></param>
        /// <param name="elseExpression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> WhereNotEmpty(Guid condition, Expression<Func<TEntity, bool>> ifExpression, Expression<Func<TEntity, bool>> elseExpression);

        /// <summary>
        /// GUID不为空的时候添加ifExpression，反之添加elseExpression
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ifWhereSql"></param>
        /// <param name="elseWhereSql"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> WhereNotEmpty(Guid condition, string ifWhereSql, string elseWhereSql);

        /// <summary>
        /// NOT IN 查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> WhereNotIn<TKey>(Expression<Func<TEntity, TKey>> key, IEnumerable<TKey> list);

        /// <summary>
        /// 子查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key">列</param>
        /// <param name="queryOperator">运算逻辑</param>
        /// <param name="queryable">子查询的查询构造器</param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> Where<TKey>(Expression<Func<TEntity, TKey>> key, QueryOperator queryOperator, INetSqlQueryable queryable);

        #endregion

        #region ==Limit==

        /// <summary>
        /// 限制
        /// </summary>
        /// <param name="skip">跳过前几条数据</param>
        /// <param name="take">取前几条数据</param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> Limit(int skip, int take);

        #endregion

        #region ==Select==

        /// <summary>
        /// 查询返回指定列
        /// </summary>
        /// <returns></returns>
        INetSqlQueryable<TEntity> Select<TResult>(Expression<Func<TEntity, TResult>> expression);

        /// <summary>
        /// 查询排除指定列
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> SelectExclude<TResult>(Expression<Func<TEntity, TResult>> expression);

        #endregion

        #region ==Join==

        /// <summary>
        /// 左连接
        /// </summary>
        /// <param name="onExpression">on表达式</param>
        /// <param name="tableName">自定义表名</param>
        /// <param name="noLock">针对SqlServer的NoLock特性，默认开启</param>
        /// <typeparam name="TEntity2"></typeparam>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2> LeftJoin<TEntity2>(Expression<Func<TEntity, TEntity2, bool>> onExpression, string tableName = null, bool noLock = true) where TEntity2 : IEntity, new();

        /// <summary>
        /// 内连接
        /// </summary>
        /// <typeparam name="TEntity2"></typeparam>
        /// <param name="onExpression"></param>
        /// <param name="tableName">自定义表名</param>
        /// <param name="noLock">针对SqlServer的NoLock特性，默认开启</param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2> InnerJoin<TEntity2>(Expression<Func<TEntity, TEntity2, bool>> onExpression, string tableName = null, bool noLock = true) where TEntity2 : IEntity, new();

        /// <summary>
        /// 右连接
        /// </summary>
        /// <typeparam name="TEntity2"></typeparam>
        /// <param name="onExpression"></param>
        /// <param name="tableName">自定义表名</param>
        /// <param name="noLock">针对SqlServer的NoLock特性，默认开启</param>
        /// <returns></returns>
        INetSqlQueryable<TEntity, TEntity2> RightJoin<TEntity2>(Expression<Func<TEntity, TEntity2, bool>> onExpression, string tableName = null, bool noLock = true) where TEntity2 : IEntity, new();

        #endregion

        #region ==Delete==

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        bool Delete();

        /// <summary>
        /// 删除
        /// <para>数据不存在也是返回true</para>
        /// </summary>
        /// <returns></returns>
        Task<bool> DeleteAsync();

        /// <summary>
        /// 删除数据返回影响条数
        /// </summary>
        /// <returns></returns>
        int DeleteWithAffectedNum();

        /// <summary>
        /// 删除数据返回影响条数
        /// </summary>
        /// <returns></returns>
        Task<int> DeleteWithAffectedNumAsync();

        #endregion

        #region ==SoftDelete==

        /// <summary>
        /// 软删除
        /// <para>数据不存在也是返回true</para>
        /// </summary>
        /// <returns></returns>
        bool SoftDelete();

        /// <summary>
        /// 软删除
        /// <para>数据不存在也是返回true</para>
        /// </summary>
        /// <returns></returns>
        Task<bool> SoftDeleteAsync();

        /// <summary>
        /// 软删除,返回影响条数
        /// </summary>
        /// <returns></returns>
        int SoftDeleteWithAffectedNum();

        /// <summary>
        /// 软删除,返回影响条数
        /// </summary>
        /// <returns></returns>
        Task<int> SoftDeleteWithAffectedNumAsync();

        #endregion

        #region ==Update==

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="setModifiedBy">设置修改人信息，默认true</param>
        /// <returns></returns>
        bool Update(Expression<Func<TEntity, TEntity>> expression, bool setModifiedBy = true);

        /// <summary>
        /// 更新
        /// <para>数据不存在也是返回true</para>
        /// </summary>
        /// <param name="updateSql">手写更新sql，只包含更新部分，如 Title=@Title</param>
        /// <param name="setModifiedBy">设置修改人信息，默认true</param>
        /// <param name="parameterObject">参数对象</param>
        /// <returns></returns>
        bool Update(string updateSql, bool setModifiedBy = true, object parameterObject = null);

        /// <summary>
        /// 更新
        /// <para>数据不存在也是返回true</para>
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="setModifiedBy">设置修改人信息，默认true</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Expression<Func<TEntity, TEntity>> expression, bool setModifiedBy = true);

        /// <summary>
        /// 更新
        /// <para>数据不存在也是返回true</para>
        /// </summary>
        /// <param name="updateSql">手写更新sql</param>
        /// <param name="setModifiedBy">设置修改人信息，默认true</param>
        /// <param name="parameterObject">参数对象</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(string updateSql, bool setModifiedBy = true, object parameterObject = null);

        /// <summary>
        /// 更新数据返回影响条数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="setModifiedBy">设置修改人信息，默认true</param>
        /// <param name="updateSql">更新SQL语句，优于表达式</param>
        /// <param name="parameterObject">参数对象</param>
        /// <returns></returns>
        int UpdateWithAffectedNum(Expression<Func<TEntity, TEntity>> expression, bool setModifiedBy = true, string updateSql = null, object parameterObject = null);

        /// <summary>
        /// 更新数据返回影响条数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="setModifiedBy">设置修改人信息，默认true</param>
        /// <param name="updateSql">更新SQL语句，优于表达式</param>
        /// <param name="parameterObject">参数对象</param>
        /// <returns></returns>
        Task<int> UpdateWithAffectedNumAsync(Expression<Func<TEntity, TEntity>> expression, bool setModifiedBy = true, string updateSql = null, object parameterObject = null);

        #endregion

        #region ==Max==

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression);

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression);

        #endregion

        #region ==Min==

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression);

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression);

        #endregion

        #region ==Sum==

        /// <summary>
        /// 求和
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        TResult Sum<TResult>(Expression<Func<TEntity, TResult>> expression);

        /// <summary>
        /// 求和
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TResult> SumAsync<TResult>(Expression<Func<TEntity, TResult>> expression);

        #endregion

        #region ==Avg==

        /// <summary>
        /// 求平均值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        TResult Avg<TResult>(Expression<Func<TEntity, TResult>> expression);

        /// <summary>
        /// 求平均值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TResult> AvgAsync<TResult>(Expression<Func<TEntity, TResult>> expression);

        #endregion

        #region ==GroupBy==

        /// <summary>
        /// 分组
        /// </summary>
        /// <returns></returns>
        IGroupByQueryable1<TResult, TEntity> GroupBy<TResult>(Expression<Func<TEntity, TResult>> expression);

        /// <summary>
        /// 分组(group by 1)
        /// </summary>
        /// <returns></returns>
        IGroupByQueryable1<INetSqlGroupingKey1<TEntity>, TEntity> GroupBy();

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
        /// <param name="paging"></param>
        /// <returns></returns>
        new IList<TEntity> Pagination(Paging paging = null);

        /// <summary>
        /// 分页查询，返回实体类型
        /// </summary>
        /// <param name="paging"></param>
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
        INetSqlQueryable<TEntity> IncludeDeleted();

        #endregion

        #region ==NotFilterTenant==

        /// <summary>
        /// 不过滤租户
        /// </summary>
        /// <returns></returns>
        INetSqlQueryable<TEntity> NotFilterTenant();

        #endregion

        #region ==Copy==

        /// <summary>
        /// 复制一个新的实例
        /// </summary>
        /// <returns></returns>
        INetSqlQueryable<TEntity> Copy();

        #endregion
    }
}
