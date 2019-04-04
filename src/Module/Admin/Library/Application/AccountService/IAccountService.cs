using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.AccountService.ViewModels;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.Permission;

namespace NetModular.Module.Admin.Application.AccountService
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
        Task<ResultModel<Account>> Login(LoginModel model);

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> LoginInfo();

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
        Task<IResultModel> Add(AccountAddModel model);

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
        /// <returns></returns>
        Task<IResultModel> Delete(Guid id);

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
        Task<List<Permission>> QueryPermissionList(Guid id);

        /// <summary>
        /// 清除指定账户的缓存数据
        /// </summary>
        void ClearPermissionListCache(Guid id);
    }
}
