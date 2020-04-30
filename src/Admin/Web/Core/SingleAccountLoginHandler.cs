using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Web;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Module.Admin.Application.AuthService;

namespace NetModular.Module.Admin.Web.Core
{
    [Singleton]
    public class SingleAccountLoginHandler : ISingleAccountLoginHandler
    {
        private readonly IAuthService _authService;
        private readonly ILoginInfo _loginInfo;

        public SingleAccountLoginHandler(IAuthService authService, ILoginInfo loginInfo)
        {
            _authService = authService;
            _loginInfo = loginInfo;
        }

        public async Task<bool> Validate()
        {
            var authInfo = await _authService.GetAuthInfo(_loginInfo.AccountId, _loginInfo.Platform);
            if (authInfo != null && authInfo.LoginTime != _loginInfo.LoginTime)
            {
                return true;
            }

            return false;
        }
    }
}
