using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.AccountPermissionResolver
{
    /// <summary>
    /// 账户权限解析接口
    /// </summary>
    public interface IAccountPermissionResolver
    {
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="accountId">账户编号</param>
        /// <param name="platform">平台</param>
        /// <returns></returns>
        Task<IList<string>> Resolve(Guid accountId, Platform platform);

        /// <summary>
        /// 解析菜单列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<IList<AccountMenuItem>> ResolveMenus(Guid accountId);

        /// <summary>
        /// 解析页面编码列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<IList<string>> ResolvePages(Guid accountId);

        /// <summary>
        /// 解析按钮编码列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<IList<string>> ResolveButtons(Guid accountId);
    }
}
