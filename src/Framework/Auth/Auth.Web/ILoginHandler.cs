using System.Security.Claims;
using Nm.Lib.Utils.Core.Result;

namespace Nm.Lib.Auth.Web
{
    /// <summary>
    /// 登录处理
    /// </summary>
    public interface ILoginHandler
    {
        IResultModel Hand(Claim[] claims);
    }
}
