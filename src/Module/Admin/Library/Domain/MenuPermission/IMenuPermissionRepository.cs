using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Domain.MenuPermission
{
    /// <summary>
    /// 菜单权限仓储
    /// </summary>
    public interface IMenuPermissionRepository : IRepository<MenuPermissionEntity>
    {
        /// <summary>
        /// 判断是否存在绑定了指定权限的菜单
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        Task<bool> ExistsBindPermission(Guid permissionId);

        /// <summary>
        /// 通过权限编号删除所有对应菜单与权限关系
        /// </summary>
        /// <param name="permissionId">权限编号</param>
        /// <returns></returns>
        Task<bool> DeleteByPermissionId(Guid permissionId);

        /// <summary>
        /// 通过菜单编号删除所有对应菜单与权限关系
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Task<bool> DeleteByMenuId(Guid menuId);

        /// <summary>
        /// 通过菜单编号获取所有对应菜单与权限关系
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Task<IList<MenuPermissionEntity>> GetListByMenuId(Guid menuId);
    }
}
