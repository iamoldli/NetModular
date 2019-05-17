using System;
using System.Threading.Tasks;
using Nm.Lib.Utils.Core.Models;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.CodeGenerator.Application.PropertyService.ViewModels;
using Nm.Module.CodeGenerator.Domain.Property.Models;

namespace Nm.Module.CodeGenerator.Application.PropertyService
{
    public interface IPropertyService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(PropertyQueryModel model);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(PropertyAddModel model);

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
        Task<IResultModel> Update(PropertyUpdateModel model);

        /// <summary>
        /// 查询排序列表
        /// </summary>
        /// <param name="classId">类编号</param>
        /// <returns></returns>
        Task<IResultModel> QuerySortList(Guid classId);

        /// <summary>
        /// 更新排序信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> UpdateSortList(SortUpdateModel<Guid> model);

        /// <summary>
        /// 更改属性可空状态
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> UpdateNullable(PropertyUpdateNullableModel model);

        /// <summary>
        /// 获取下拉列表
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        Task<IResultModel> Select(Guid classId);

        /// <summary>
        /// 更改属性可空状态
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> UpdateShowInList(PropertyUpdateShowInListModel model);
    }
}
