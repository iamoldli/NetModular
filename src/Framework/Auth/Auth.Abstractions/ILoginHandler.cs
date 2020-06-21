using System.Collections.Generic;
using System.Security.Claims;

namespace NetModular.Lib.Auth.Abstractions
{
    /// <summary>
    /// 登录处理
    /// </summary>
    public interface ILoginHandler
    {
        /// <summary>
        /// 登录处理
        /// </summary>
        /// <param name="claims">信息</param>
        /// <param name="extendData">扩展数据</param>
        /// <returns></returns>
        IResultModel Hand(List<Claim> claims, string extendData);
    }
}
