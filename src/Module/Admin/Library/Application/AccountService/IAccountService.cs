using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.Admin.Application.AccountService.ViewModels;
using Tm.Module.Admin.Domain.Account;
using Tm.Module.Admin.Domain.Account.Models;
using Tm.Module.Admin.Domain.Permission;

namespace Tm.Module.Admin.Application.AccountService
{
    /// <summary>
    /// 账户服务
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// 创建验证码图片
        /// </summary>
        /// <param name="length">验证码长度</param>
        /// <returns></returns>
        IResultModel CreateVerifyCode(int length = 6);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ResultModel<AccountEntity>> Login(LoginModel model);

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> LoginInfo(Guid accountId);

        /// <summary>
        /// 获取CID
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>guid</returns>
        Task<Guid> GetCID(Guid accountId);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> UpdatePassword(UpdatePasswordModel model);

        /// <summary>
        /// 绑定角色
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> BindRole(AccountRoleBindModel model);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(AccountQueryModel model);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel<Guid>> Add(AccountAddModel model);

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
        Task<IResultModel> Update(AccountUpdateModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deleter">删除人</param>
        /// <returns></returns>
        Task<IResultModel> Delete(Guid id,Guid deleter);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> ResetPassword(Guid id);

        /// <summary>
        /// 查询指定账户的权限列表
        /// </summary>
        /// <returns></returns>
        Task<List<PermissionEntity>> QueryPermissionList(Guid id);

        /// <summary>
        /// 清除指定账户的缓存数据
        /// </summary>
        void ClearPermissionListCache(Guid id);

        /// <summary>
        /// 密码加密
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        string EncryptPassword(string userName, string password);
    }
}
