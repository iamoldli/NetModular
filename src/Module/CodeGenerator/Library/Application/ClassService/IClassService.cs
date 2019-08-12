using System;
using System.Threading.Tasks;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.CodeGenerator.Application.ClassService.ViewModels;
using Tm.Module.CodeGenerator.Application.ProjectService.ResultModels;
using Tm.Module.CodeGenerator.Domain.Class.Models;

namespace Tm.Module.CodeGenerator.Application.ClassService
{
    /// <summary>
    /// 类服务
    /// </summary>
    public interface IClassService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(ClassQueryModel model);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(ClassAddModel model);

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
        Task<IResultModel> Update(ClassUpdateModel model);

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel<ProjectBuildCodeResultModel>> BuildCode(Guid id);
    }
}