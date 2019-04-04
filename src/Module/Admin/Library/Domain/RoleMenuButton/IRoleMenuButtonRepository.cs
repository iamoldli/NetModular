using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Domain.RoleMenuButton
{
    public interface IRoleMenuButtonRepository : IRepository<RoleMenuButton>
    {
        /// <summary>
        /// 根据角色删除
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<bool> DeleteByRole(Guid roleId);

        /// <summary>
        /// 根据菜单删除
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Task<bool> DeleteByMenu(Guid menuId);

        /// <summary>
        /// 根据按钮删除
        /// </summary>
        /// <param name="buttonId"></param>
        /// <returns></returns>
        Task<bool> DeleteByButton(Guid buttonId);

        /// <summary>
        /// 获取角色的菜单关联的按钮信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Task<IList<Button.Button>> Query(Guid roleId, Guid menuId);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Exists(RoleMenuButton entity);

        /// <summary>
        /// 根据实体删除单个数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Delete(RoleMenuButton entity);

        /// <summary>
        /// 根据角色和菜单删除所有关联的按钮
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Task<bool> Delete(Guid roleId, Guid menuId);

        /// <summary>
        /// 判断是否存在指定按钮的关联数据
        /// </summary>
        /// <param name="buttonId"></param>
        /// <returns></returns>
        Task<bool> ExistsWidthButton(Guid buttonId);
    }
}
