using System.Security.Claims;
using NetModular.Lib.Utils.Core.Result;

namespace NetModular.Lib.Auth.Abstractions
{
    /// <summary>
    /// 登录处理
    /// </summary>
    public interface ILoginHandler
    {
        IResultModel Hand(Claim[] claims);
    }
}
