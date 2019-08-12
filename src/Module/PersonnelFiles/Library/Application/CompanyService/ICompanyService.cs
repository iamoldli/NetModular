using System;
using System.Threading.Tasks;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.PersonnelFiles.Application.CompanyService.ViewModels;
using Tm.Module.PersonnelFiles.Domain.Company.Models;

namespace Tm.Module.PersonnelFiles.Application.CompanyService
{
    /// <summary>
    /// 公司单位服务
    /// </summary>
    public interface ICompanyService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(CompanyQueryModel model);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(CompanyAddModel model);

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
        Task<IResultModel> Update(CompanyUpdateModel model);

        /// <summary>
        /// 下拉列表
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Select();
    }
}
