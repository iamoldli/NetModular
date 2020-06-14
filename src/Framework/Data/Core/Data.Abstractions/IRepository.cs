using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions.Entities;

namespace NetModular.Lib.Data.Abstractions
{
    /// <summary>
    /// 泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : IEntity, new()
    {
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
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> where, IUnitOfWork uow);

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
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> where, IUnitOfWork uow);

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
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        bool Exists(dynamic id, IUnitOfWork uow);

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
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(dynamic id, IUnitOfWork uow);

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
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        bool Add(TEntity entity, IUnitOfWork uow);

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
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        Task<bool> AddAsync(TEntity entity, IUnitOfWork uow);

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
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        bool Add(List<TEntity> list, IUnitOfWork uow);

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
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        Task<bool> AddAsync(List<TEntity> list, IUnitOfWork uow);

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
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        bool Delete(dynamic id, IUnitOfWork uow);

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
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(dynamic id, IUnitOfWork uow);

        #endregion

        #region ==Update==

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool Update(TEntity entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        bool Update(TEntity entity, IUnitOfWork uow);

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
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity, IUnitOfWork uow);

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
        /// <param name="uow">工作单元</param>
        /// <param name="rowLock">行锁</param>
        /// <param name="noLock">SqlServer的NOLOCK</param>
        /// <returns></returns>
        TEntity Get(dynamic id, IUnitOfWork uow, bool rowLock = false, bool noLock = true);

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
        /// <param name="uow">工作单元</param>
        /// <param name="rowLock">行锁</param>
        /// <param name="noLock">SqlServer的NOLOCK</param>
        /// <returns></returns>
        Task<TEntity> GetAsync(dynamic id, IUnitOfWork uow, bool rowLock = false, bool noLock = true);

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
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        IList<TEntity> GetAll(IUnitOfWork uow);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<IList<TEntity>> GetAllAsync();

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        Task<IList<TEntity>> GetAllAsync(IUnitOfWork uow);

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
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        bool SoftDelete(dynamic id, IUnitOfWork uow);

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
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        Task<bool> SoftDeleteAsync(dynamic id, IUnitOfWork uow);

        #endregion

        #region ==Clear==

        /// <summary>
        /// 清空数据
        /// </summary>
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        bool Clear(IUnitOfWork uow = null);

        /// <summary>
        /// 清空数据
        /// </summary>
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        Task<bool> ClearAsync(IUnitOfWork uow = null);

        #endregion

        #region ==表操作==

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="tableName">指定表名称</param>
        /// <returns></returns>
        Task CreateTable(string tableName = null);

        #endregion
    }
}
