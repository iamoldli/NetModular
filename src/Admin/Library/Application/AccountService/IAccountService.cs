using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Admin.Application.AccountService.ViewModels;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.Account.Models;

namespace NetModular.Module.Admin.Application.AccountService
{
    /// <summary>
    /// 账户服务
    /// </summary>
    public interface IAccountService
    {
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
        /// <param name="uow"></param>
        /// <returns></returns>
        Task<IResultModel<Guid>> Add(AccountAddModel model, IUnitOfWork uow = null);

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
        Task<IResultModel> Delete(Guid id, Guid deleter);

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
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> ResetPassword(Guid id);

        /// <summary>
        /// 查询指定账户和平台的权限编码列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        Task<IList<string>> QueryPermissionList(Guid id, Platform platform);

        /// <summary>
        /// 清除指定账户的缓存数据
        /// </summary>
        void ClearPermissionListCache(Guid id);

        /// <summary>
        /// 皮肤修改
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> SkinUpdate(Guid id, AccountSkinUpdateModel model);

        /// <summary>
        /// 查询账户(缓存优先)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AccountEntity> Get(Guid id);

        /// <summary>
        /// 账户信息同步，用于从其他扩展模块中同步账户信息，比如人事档案模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Sync(AccountSyncModel model);
    }
}
