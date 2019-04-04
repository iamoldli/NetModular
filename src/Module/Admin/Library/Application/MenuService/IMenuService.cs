using System;
using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.MenuService.ViewModels;

namespace NetModular.Module.Admin.Application.MenuService
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public interface IMenuService
    {
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> GetTree();

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Add(MenuAddModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Delete(Guid id);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Edit(Guid id);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Update(MenuUpdateModel model);

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Query(MenuQueryModel model);

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Details(Guid id);

        /// <summary>
        /// 查询菜单的权限列表
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> PermissionList(Guid id);

        /// <summary>
        /// 绑定权限
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> BindPermission(MenuBindPermissionModel model);

        /// <summary>
        /// 获取菜单的所有按钮列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> ButtonList(Guid id);
    }
}
