using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace NetModular.Lib.Auth.Abstractions
{
    /// <summary>
    /// 登录Claims扩展处理器
    /// </summary>
    public interface ILoginClaimsExtendProvider
    {
        /// <summary>
        /// 获取扩展Claims列表
        /// </summary>
        /// <param name="accountId">账户编号</param>
        /// <returns></returns>
        List<Claim> GetExtendClaims(Guid accountId);
    }
}
