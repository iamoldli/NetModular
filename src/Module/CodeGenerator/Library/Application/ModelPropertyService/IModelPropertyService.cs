using System;
using System.Threading.Tasks;
using Tm.Lib.Utils.Core.Models;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.CodeGenerator.Application.ModelPropertyService.ViewModels;
using Tm.Module.CodeGenerator.Domain.ModelProperty.Models;

namespace Tm.Module.CodeGenerator.Application.ModelPropertyService
{
    public interface IModelPropertyService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(ModelPropertyQueryModel model);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(ModelPropertyAddModel model);

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
        Task<IResultModel> Update(ModelPropertyUpdateModel model);

        /// <summary>
        /// 查询排序列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> QuerySortList(ModelPropertyQueryModel model);

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
        Task<IResultModel> UpdateNullable(ModelPropertyUpdateNullableModel model);

        /// <summary>
        /// 查询下拉列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Select(ModelPropertyQueryModel model);

        /// <summary>
        /// 从实体中导入属性
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> ImportFromEntity(ModelPropertyImportModel model);
    }
}
