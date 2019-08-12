using System;
using System.Data;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;

namespace Tm.Module.Admin.Domain.ButtonPermission
{
    /// <summary>
    /// 按钮权限仓储
    /// </summary>
    public interface IButtonPermissionRepository : IRepository<ButtonPermissionEntity>
    {
        /// <summary>
        /// 判断是否存在绑定了指定权限的按钮
        /// </summary>
        /// <param name="permissionCode"></param>
        /// <returns></returns>
        Task<bool> ExistsBindPermission(string permissionCode);


        /// <summary>
        /// 通过按钮编号删除所有对应菜单与权限关系
        /// </summary>
        /// <param name="buttonCode"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<bool> DeleteByButton(string buttonCode, IDbTransaction transaction);
    }
}
