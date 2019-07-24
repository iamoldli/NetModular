using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions.Entities;
using Nm.Lib.Data.Abstractions.SqlQueryable;

namespace Nm.Lib.Data.Abstractions
{
    public interface IDbSet
    {
        #region ==属性==

        /// <summary>
        /// 数据库上下文
        /// </summary>
        IDbContext DbContext { get; }

        /// <summary>
        /// 实体信息
        /// </summary>
        IEntityDescriptor EntityDescriptor { get; }

        #endregion

        #region ==Execute==

        /// <summary>
        /// 执行一个命令并返回受影响的行数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>

        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        int Execute(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// 执行一个命令并返回受影响的行数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        Task<int> ExecuteAsync(string sql, object param = null, CommandType? commandType = null);

        #endregion

        #region ==ExecuteScalar==

        /// <summary>
        /// 执行一个命令并返回单个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        T ExecuteScalar<T>(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// 执行一个命令并返回单个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        Task<T> ExecuteScalarAsync<T>(string sql, object param = null, CommandType? commandType = null);

        #endregion

        #region ==QueryFirstOrDefault==

        /// <summary>
        /// 查询第一条数据，不存在返回默认值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        dynamic QueryFirstOrDefault(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// 查询第一条数据，不存在返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        T QueryFirstOrDefault<T>(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// 查询第一条数据，不存在返回默认值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        Task<dynamic> QueryFirstOrDefaultAsync(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// 查询第一条数据，不存在返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType? commandType = null);

        #endregion

        #region ==QuerySingleOrDefault==

        /// <summary>
        /// 查询单条记录，不存在返回默认值，如果存在多条记录则抛出异常
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        dynamic QuerySingleOrDefault(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// 查询单条记录，不存在返回默认值，如果存在多条记录则抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        T QuerySingleOrDefault<T>(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// 查询单条记录，不存在返回默认值，如果存在多条记录则抛出异常
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        Task<dynamic> QuerySingleOrDefaultAsync(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// 查询单条记录，不存在返回默认值，如果存在多条记录则抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, CommandType? commandType = null);

        #endregion

        #region ==Query==

        /// <summary>
        /// 查询数据，返回匿名对象
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        IEnumerable<dynamic> Query(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// 查询数据，返回匿名数据
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType? commandType = null);



        #endregion
    }

    /// <summary>
    /// 数据集
    /// </summary>
    public interface IDbSet<TEntity> : IDbSet where TEntity : IEntity, new()
    {
        #region ==Insert==

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="tableName">指定表名称</param>
        /// <returns></returns>
        bool Insert(TEntity entity, string tableName = null);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="tableName">指定表名称</param>
        /// <returns></returns>
        Task<bool> InsertAsync(TEntity entity, string tableName = null);

        #endregion

        #region ==BatchInsert==

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entityList">实体集合</param>
        /// <param name="flushSize">单次刷新数量</param>
        /// <param name="tableName">指定表名称</param>
        /// <returns></returns>
        bool BatchInsert(List<TEntity> entityList, int flushSize = 10000, string tableName = null);

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entityList">实体集合</param>
        /// <param name="flushSize">单次刷新数量</param>
        /// <param name="tableName">指定表名称</param>
        /// <returns></returns>
        Task<bool> BatchInsertAsync(List<TEntity> entityList, int flushSize = 10000, string tableName = null);

        #endregion

        #region ==Delete==

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableName">指定表名称</param>
        /// <returns></returns>
        bool Delete(dynamic id, string tableName = null);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableName">指定表名称</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(dynamic id, string tableName = null);

        #endregion

        #region ==SoftDelete==

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableName">指定表名称</param>
        /// <returns></returns>
        bool SoftDelete(dynamic id, string tableName = null);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableName">指定表名称</param>
        /// <returns></returns>
        Task<bool> SoftDeleteAsync(dynamic id, string tableName = null);

        #endregion

        #region ==Update==

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="tableName">指定表名称</param>
        /// <returns></returns>
        bool Update(TEntity entity, string tableName = null);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="tableName">指定表名称</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity, string tableName = null);

        #endregion

        #region ==Get==

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableName">指定表名称</param>
        /// <returns></returns>
        TEntity Get(dynamic id, string tableName = null);

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableName">指定表名称</param>
        /// <returns></returns>
        Task<TEntity> GetAsync(dynamic id, string tableName = null);

        #endregion

        #region Exists==

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="tableName">指定表名称</param>
        /// <returns></returns>
        bool Exists(dynamic id, string tableName = null);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="tableName">指定表名称</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(dynamic id, string tableName = null);

        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="expression">过滤条件</param>
        /// <param name="tableName">指定表名称</param>
        /// <returns></returns>
        INetSqlQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression = null, string tableName = null);
    }
}
