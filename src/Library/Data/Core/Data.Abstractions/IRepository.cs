using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions.Entities;

namespace Nm.Lib.Data.Abstractions
{
    /// <summary>
    /// 泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : IEntity, new()
    {
        #region ==Transaction==

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        IDbTransaction BeginTransaction();

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="isolationLevel">隔离级别</param>
        /// <returns></returns>
        IDbTransaction BeginTransaction(IsolationLevel isolationLevel);

        #endregion

        #region ==Exists==

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> where, IDbTransaction transaction);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> where, IDbTransaction transaction);

        /// <summary>
        /// 根据id判断是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Exists(dynamic id);


        /// <summary>
        /// 根据id判断是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        bool Exists(dynamic id, IDbTransaction transaction);

        /// <summary>
        /// 根据id判断是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(dynamic id);

        /// <summary>
        /// 根据id判断是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(dynamic id, IDbTransaction transaction);

        #endregion

        #region ==Add==

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool Add(TEntity entity);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        bool Add(TEntity entity, IDbTransaction transaction);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<bool> AddAsync(TEntity entity);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        Task<bool> AddAsync(TEntity entity, IDbTransaction transaction);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list">实体集合</param>
        /// <returns></returns>
        bool Add(List<TEntity> list);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list">实体集合</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        bool Add(List<TEntity> list, IDbTransaction transaction);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list">实体集合</param>
        /// <returns></returns>
        Task<bool> AddAsync(List<TEntity> list);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list">实体集合</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        Task<bool> AddAsync(List<TEntity> list, IDbTransaction transaction);

        #endregion

        #region ==Delete==

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(dynamic id);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        bool Delete(dynamic id, IDbTransaction transaction);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(dynamic id);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(dynamic id, IDbTransaction transaction);

        #endregion

        #region ==Update==

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns
        bool Update(TEntity entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        bool Update(TEntity entity, IDbTransaction transaction);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity, IDbTransaction transaction);

        #endregion

        #region ==Get==

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(dynamic id);

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        TEntity Get(dynamic id, IDbTransaction transaction);

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(dynamic id);

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        Task<TEntity> GetAsync(dynamic id, IDbTransaction transaction);

        #endregion

        #region ==GetAll==

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        IList<TEntity> GetAll();

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        IList<TEntity> GetAll(IDbTransaction transaction);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<IList<TEntity>> GetAllAsync();

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        Task<IList<TEntity>> GetAllAsync(IDbTransaction transaction);

        #endregion

        #region ==SoftDelete==

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        bool SoftDelete(dynamic id);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        bool SoftDelete(dynamic id, IDbTransaction transaction);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<bool> SoftDeleteAsync(dynamic id);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        Task<bool> SoftDeleteAsync(dynamic id, IDbTransaction transaction);

        #endregion
    }
}
