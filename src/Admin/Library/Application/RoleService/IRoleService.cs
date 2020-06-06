using System;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
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
        /// 查询角色绑定的页面
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IResultModel> QueryBindPages(Guid roleId);

        /// <summary>
        /// 绑定页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> BindPages(RolePageBindModel model);

        /// <summary>
        /// 获取角色绑定的菜单列表(含按钮)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> QueryBindMenus(Guid id);

        /// <summary>
        /// 绑定菜单
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> BindMenus(RoleMenuBindModel model);

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
        /// 查询平台权限绑定列表
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="platform"></param>
        /// <returns></returns>
        Task<IResultModel> QueryPlatformPermissions(Guid roleId, Platform platform);

        /// <summary>
        /// 平台权限绑定
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> BindPlatformPermissions(RolePermissionBindModel model);
    }
}
