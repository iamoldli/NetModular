using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Admin.Domain.Permission.Models;

namespace NetModular.Module.Admin.Domain.Permission
{
    /// <summary>
    /// 权限仓储
    /// </summary>
    public interface IPermissionRepository : IRepository<PermissionEntity>
    {
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        Task<bool> Exists(PermissionEntity entity, IUnitOfWork uow);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Exists(Guid id);

        /// <summary>
        /// 根据模块编码判断是否存在
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        Task<bool> ExistsWidthModule(string moduleCode);

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        Task<IList<PermissionEntity>> Query(PermissionQueryModel model);

        /// <summary>
        /// 通过菜单查找对应权限列表
        /// </summary>
        /// <param name="menuCode"></param>
        /// <returns></returns>
        Task<IList<PermissionEntity>> QueryByMenu(string menuCode);

        /// <summary>
        /// 通过按钮编号查找对应权限列表
        /// </summary>
        /// <param name="buttonCode"></param>
        /// <returns></returns>
        Task<IList<PermissionEntity>> QueryByButton(string buttonCode);

        /// <summary>
        /// 查询指定账户的权限列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<IList<PermissionEntity>> QueryByAccount(Guid accountId);

        /// <summary>
        /// 修改同步信息
        /// </summary>
        /// <param name="permission"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        Task<bool> UpdateForSync(PermissionEntity permission, IUnitOfWork uow);
    }
}
