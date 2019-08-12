using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;

namespace Tm.Module.Admin.Domain.MenuPermission
{
    /// <summary>
    /// 菜单权限仓储
    /// </summary>
    public interface IMenuPermissionRepository : IRepository<MenuPermissionEntity>
    {
        /// <summary>
        /// 判断是否存在绑定了指定权限的菜单
        /// </summary>
        /// <param name="permissionCode">权限编码</param>
        /// <returns></returns>
        Task<bool> ExistsBindPermission(string permissionCode);

        /// <summary>
        /// 通过权限编号删除所有对应菜单与权限关系
        /// </summary>
        /// <param name="permissionCode">权限编码</param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<bool> DeleteByPermission(string permissionCode, IDbTransaction transaction);

        /// <summary>
        /// 通过菜单编号删除所有对应菜单与权限关系
        /// </summary>
        /// <param name="menuCode"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<bool> DeleteByMenu(string menuCode, IDbTransaction transaction);

        /// <summary>
        /// 通过菜单编号获取所有对应菜单与权限关系
        /// </summary>
        /// <param name="menuCode"></param>
        /// <returns></returns>
        Task<IList<MenuPermissionEntity>> GetListByMenu(string menuCode);
    }
}
