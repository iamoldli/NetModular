using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Web;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Lib.Utils.Core.Enums;
using NetModular.Module.Admin.Infrastructure.AccountPermissionResolver;

namespace NetModular.Module.Admin.Web.Core
{
    /// <summary>
    /// 权限验证
    /// </summary>
    [Singleton]
    public class PermissionValidateHandler : IPermissionValidateHandler
    {
        private readonly ILoginInfo _loginInfo;
        private readonly IAccountPermissionResolver _permissionResolver;

        public PermissionValidateHandler(ILoginInfo loginInfo, IAccountPermissionResolver permissionResolver)
        {
            _loginInfo = loginInfo;
            _permissionResolver = permissionResolver;
        }

        public async Task<bool> Validate(IDictionary<string, string> routeValues, HttpMethod httpMethod)
        {
            var permissions = await _permissionResolver.Resolve(_loginInfo.AccountId, _loginInfo.Platform);

            var area = routeValues["area"];
            var controller = routeValues["controller"];
            var action = routeValues["action"];
            return permissions.Any(m => m.EqualsIgnoreCase($"{area}_{controller}_{action}_{httpMethod}"));
        }
    }
}
