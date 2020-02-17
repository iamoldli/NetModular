using System.Security.Claims;
using NetModular.Lib.Utils.Core.Result;

namespace NetModular.Lib.Auth.Web
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
        IResultModel Hand(Claim[] claims, string extendData);
    }
}
