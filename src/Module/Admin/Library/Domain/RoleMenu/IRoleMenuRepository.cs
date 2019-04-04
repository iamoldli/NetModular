using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Domain.RoleMenu
{
    /// <summary>
    /// 角色菜单仓储
    /// </summary>
    public interface IRoleMenuRepository : IRepository<RoleMenu>
    {
        /// <summary>
        /// 根据角色查询关联列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IList<RoleMenu>> GetByRoleId(Guid roleId);

        /// <summary>
        /// 根据角色编号删除对应关联信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<bool> DeleteByRoleId(Guid roleId);

        /// <summary>
        /// 根据菜单编号删除对应关联信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Task<bool> DeleteByMenuId(Guid menuId);

        /// <summary>
        /// 判断是否存在指定菜单的关联
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Task<bool> ExistsWidthMenuId(Guid menuId);
    }
}
