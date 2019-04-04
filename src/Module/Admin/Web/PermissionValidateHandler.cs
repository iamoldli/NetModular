using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Module.Admin.Application.AccountService;

namespace NetModular.Module.Admin.Web
{
    /// <summary>
    /// 权限验证
    /// </summary>
    public class PermissionValidateHandler : IPermissionValidateHandler
    {

        private readonly LoginInfo _loginInfo;
        private readonly IAccountService _accountService;

        public PermissionValidateHandler(IAccountService accountService, LoginInfo loginInfo)
        {
            _accountService = accountService;
            _loginInfo = loginInfo;
        }

        public bool Validate(AuthorizationFilterContext context)
        {
            var permissions = _accountService.QueryPermissionList(_loginInfo.AccountId).Result;

            var routeValues = context.ActionDescriptor.RouteValues;
            var area = routeValues["area"];
            var controller = routeValues["controller"];
            var action = routeValues["action"];

            return permissions.Any(m => m.ModuleCode.EqualsIgnoreCase(area) && m.Controller.EqualsIgnoreCase(controller) && m.Action.EqualsIgnoreCase(action));
        }
    }
}
