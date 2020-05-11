using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Web;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Lib.Utils.Core.Enums;
using NetModular.Module.Admin.Application.AccountService;

namespace NetModular.Module.Admin.Web.Core
{
    /// <summary>
    /// 权限验证
    /// </summary>
    [Singleton]
    public class PermissionValidateHandler : IPermissionValidateHandler
    {
        private readonly ILoginInfo _loginInfo;
        private readonly IAccountService _accountService;

        public PermissionValidateHandler(IAccountService accountService, ILoginInfo loginInfo)
        {
            _accountService = accountService;
            _loginInfo = loginInfo;
        }

        public async Task<bool> Validate(IDictionary<string, string> routeValues, HttpMethod httpMethod)
        {
            var permissions = await _accountService.QueryPermissionList(_loginInfo.AccountId, _loginInfo.Platform);

            var area = routeValues["area"];
            var controller = routeValues["controller"];
            var action = routeValues["action"];
            return permissions.Any(m => m.EqualsIgnoreCase($"{area}_{controller}_{action}_{httpMethod}"));
        }
    }
}
