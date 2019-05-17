using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.Admin.Application.AccountService;
using Nm.Module.Admin.Domain.Permission;
using Nm.Module.Admin.Infrastructure.Options;

namespace Nm.Module.Admin.Web.Core
{
    /// <summary>
    /// 权限验证
    /// </summary>
    public class PermissionValidateHandler : IPermissionValidateHandler
    {
        private readonly AdminOptions _options;
        private readonly LoginInfo _loginInfo;
        private readonly IAccountService _accountService;

        public PermissionValidateHandler(IOptionsMonitor<AdminOptions> optionsAccessor, IAccountService accountService, LoginInfo loginInfo)
        {
            _options = optionsAccessor.CurrentValue;
            _accountService = accountService;
            _loginInfo = loginInfo;
        }

        public bool Validate(AuthorizationFilterContext context)
        {
            if (!_options.PermissionValidate)
                return true;

            var permissions = _accountService.QueryPermissionList(_loginInfo.AccountId).Result;

            var routeValues = context.ActionDescriptor.RouteValues;
            var area = routeValues["area"];
            var controller = routeValues["controller"];
            var action = routeValues["action"];
            var httpMethod = (HttpMethodType)Enum.Parse(typeof(HttpMethodType), context.HttpContext.Request.Method);
            return permissions.Any(m => m.ModuleCode.EqualsIgnoreCase(area) && m.Controller.EqualsIgnoreCase(controller) && m.Action.EqualsIgnoreCase(action) && m.HttpMethod == httpMethod);
        }
    }
}
