using System;
using System.Threading.Tasks;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.PersonnelFiles.Application.UserService.ViewModels;
using Tm.Module.PersonnelFiles.Domain.User.Models;

namespace Tm.Module.PersonnelFiles.Application.UserService
{
    /// <summary>
    /// 用户信息服务
    /// </summary>
    public interface IUserService
    {
        #region ==基本信息==

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(UserQueryModel model);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(UserAddModel model);

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
        Task<IResultModel> Update(UserUpdateModel model);

        #endregion

        #region ==联系方式==

        /// <summary>
        /// 编辑联系方式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> EditContact(Guid id);

        /// <summary>
        /// 更新联系方式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> UpdateContact(UserContactUpdateViewModel model);

        /// <summary>
        /// 联系方式详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> ContactDetails(Guid id);

        #endregion
    }
}
