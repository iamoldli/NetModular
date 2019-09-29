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
        /// <param name="permissionCode"></param>
        /// <returns></returns>
        Task<bool> ExistsBindPermission(string permissionCode);


        /// <summary>
        /// 通过按钮编号删除所有对应菜单与权限关系
        /// </summary>
        /// <param name="buttonCode"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        Task<bool> DeleteByButton(string buttonCode, IUnitOfWork uow);
    }
}
