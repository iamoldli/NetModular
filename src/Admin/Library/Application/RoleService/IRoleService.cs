using System;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.RoleService.ViewModels;
using NetModular.Module.Admin.Domain.Role.Models;

namespace NetModular.Module.Admin.Application.RoleService
{
    public interface IRoleService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(RoleQueryModel model);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(RoleAddModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        Task<IResultModel> Delete(Guid id);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Edit(Guid id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Update(RoleUpdateModel model);

        /// <summary>
        /// 获取角色绑定的菜单列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> MenuList(Guid id);

        /// <summary>
        /// 绑定菜单
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> BindMenu(RoleMenuBindModel model);

        /// <summary>
        /// 获取绑定菜单的按钮信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Task<IResultModel> MenuButtonList(Guid id, Guid menuId);

        /// <summary>
        /// 绑定菜单按钮
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> BindMenuButton(RoleMenuButtonBindModel model);

        /// <summary>
        /// 获取角色的下拉列表
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Select();

        /// <summary>
        /// 添加指定的角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> AddSpecified(RoleAddModel model);

        /// <summary>
        /// 平台权限绑定列表
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="platform"></param>
        /// <returns></returns>
        Task<IResultModel> PlatformPermissionList(Guid roleId, Platform platform);

        /// <summary>
        /// 平台权限绑定
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> PlatformPermissionBind(RolePlatformPermissionBindModel model);
    }
}
