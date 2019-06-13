using System;
using System.Threading.Tasks;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.PersonnelFiles.Application.UserContactService.ViewModels;
using Nm.Module.PersonnelFiles.Domain.UserContact.Models;

namespace Nm.Module.PersonnelFiles.Application.UserContactService
{
    /// <summary>
    /// 用户联系信息服务
    /// </summary>
    public interface IUserContactService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(UserContactQueryModel model);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(UserContactAddModel model);

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
        Task<IResultModel> Update(UserContactUpdateModel model);

    }
}
