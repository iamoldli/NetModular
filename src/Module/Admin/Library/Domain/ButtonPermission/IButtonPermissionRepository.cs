using System;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Admin.Domain.ButtonPermission
{
    /// <summary>
    /// 按钮权限仓储
    /// </summary>
    public interface IButtonPermissionRepository : IRepository<ButtonPermissionEntity>
    {
        /// <summary>
        /// 判断是否存在绑定了指定权限的按钮
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        Task<bool> ExistsBindPermission(Guid permissionId);


        /// <summary>
        /// 通过按钮编号删除所有对应菜单与权限关系
        /// </summary>
        /// <param name="buttonId"></param>
        /// <returns></returns>
        Task<bool> RemoveByButtonId(Guid buttonId);
    }
}
