using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.CodeGenerator.Application.ProjectService.ResultModels;
using Nm.Module.CodeGenerator.Application.ProjectService.ViewModels;
using Nm.Module.CodeGenerator.Domain.Class;
using Nm.Module.CodeGenerator.Domain.Project.Models;

namespace Nm.Module.CodeGenerator.Application.ProjectService
{
    /// <summary>
    /// 项目服务
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(ProjectQueryModel model);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(ProjectAddModel model);

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
        Task<IResultModel> Update(ProjectUpdateModel model);

        /// <summary>
        /// 生成代码,返回文件的Guid
        /// </summary>
        /// <returns></returns>
        Task<IResultModel<ProjectBuildCodeResultModel>> BuildCode(ProjectBuildCodeModel model);

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="classList"></param>
        /// <returns></returns>
        Task<IResultModel<ProjectBuildCodeResultModel>> BuildCode(Guid projectId, IList<ClassEntity> classList = null);
    }
}
