using System.Threading.Tasks;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.PersonnelFiles.Application.UserEducationHistoryService.ViewModels;
using Nm.Module.PersonnelFiles.Domain.UserEducationHistory.Models;

namespace Nm.Module.PersonnelFiles.Application.UserEducationHistoryService
{
    /// <summary>
    /// 用户工作经历服务
    /// </summary>
    public interface IUserEducationHistoryService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(UserEducationHistoryQueryModel model);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(UserEducationHistoryAddModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        Task<IResultModel> Delete(int id);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Edit(int id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Update(UserEducationHistoryUpdateModel model);
    }
}
