using System;
using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Models;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.CodeGenerator.Application.EnumItemService.ViewModels;
using NetModular.Module.CodeGenerator.Domain.EnumItem.Models;

namespace NetModular.Module.CodeGenerator.Application.EnumItemService
{
    public interface IEnumItemService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(EnumItemQueryModel model);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(EnumItemAddModel model);

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
        Task<IResultModel> Update(EnumItemUpdateModel model);

        /// <summary>
        /// 查询排序列表
        /// </summary>
        /// <param name="enumId">枚举编号</param>
        /// <returns></returns>
        Task<IResultModel> QuerySortList(Guid enumId);

        /// <summary>
        /// 更新排序信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> UpdateSortList(SortUpdateModel<Guid> model);
    }
}
