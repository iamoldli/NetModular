using System.Collections.Generic;
using System.Security.Claims;
using NetModular.Module.Admin.Domain.Account;

namespace NetModular.Module.Admin.Web.Core
{
    /// <summary>
    /// 登录Claims扩展处理器
    /// </summary>
    public interface ILoginClaimsExtendProvider
    {
        /// <summary>
        /// 获取扩展Claims列表
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        List<Claim> GetExtendClaims(AccountEntity account);
    }
}
