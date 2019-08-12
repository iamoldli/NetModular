using System.Security.Claims;
using Tm.Lib.Utils.Core.Result;

namespace Tm.Lib.Auth.Web
{
    /// <summary>
    /// 登录处理
    /// </summary>
    public interface ILoginHandler
    {
        IResultModel Hand(Claim[] claims);
    }
}
