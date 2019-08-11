using System;
using System.Threading.Tasks;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.PersonnelFiles.Application.DepartmentService.ViewModels;
using Nm.Module.PersonnelFiles.Domain.Department.Models;

namespace Nm.Module.PersonnelFiles.Application.DepartmentService
{
    /// <summary>
    /// 部门服务
    /// </summary>
    public interface IDepartmentService
    {
        /// <summary>
        /// 查询指定公司的部门树
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        Task<IResultModel> GetTree(Guid companyId);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(DepartmentQueryModel model);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(DepartmentAddModel model);

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
        Task<IResultModel> Update(DepartmentUpdateModel model);

        /// <summary>
        /// 获取部门的完整路径
        /// </summary>
        /// <returns></returns>
        Task<string> GetFullPath(Guid id);
    }
}
