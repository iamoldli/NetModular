using System;
using System.Threading.Tasks;
using Nm.Lib.Utils.Core.Models;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Application.MenuService.ViewModels;
using Nm.Module.Admin.Domain.Menu.Models;

namespace Nm.Module.Admin.Application.MenuService
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
        /// 删除
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
        /// 查询排序列表
        /// </summary>
        /// <param name="parentId">父节点</param>
        /// <returns></returns>
        Task<IResultModel> QuerySortList(Guid parentId);

        /// <summary>
        /// 更新排序信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> UpdateSortList(SortUpdateModel<Guid> model);
    }
}
