using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Module.Admin.Domain.Permission.Models;

namespace Nm.Module.Admin.Domain.Permission
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
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<bool> Exists(PermissionEntity entity, IDbTransaction transaction);

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
        /// 通过菜单编号查找对应权限列表
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Task<IList<PermissionEntity>> QueryByMenu(Guid menuId);

        /// <summary>
        /// 通过按钮编号查找对应权限列表
        /// </summary>
        /// <param name="buttonId"></param>
        /// <returns></returns>
        Task<IList<PermissionEntity>> QueryByButton(Guid buttonId);

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
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<bool> UpdateForSync(PermissionEntity permission, IDbTransaction transaction);
    }
}
