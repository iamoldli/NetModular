using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Tm.Lib.Auth.Abstractions;
using Tm.Lib.Auth.Web;
using Tm.Lib.Utils.Core.Enums;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Module.Admin.Application.AccountService;
using Tm.Module.Admin.Application.SystemService;
using Tm.Module.Admin.Application.SystemService.ViewModels;
using Tm.Module.Admin.Infrastructure.Options;

namespace Tm.Module.Admin.Web.Core
{
    /// <summary>
    /// 权限验证
    /// </summary>
    public class PermissionValidateHandler : IPermissionValidateHandler
    {
        private readonly AdminOptions _options;
        private readonly ILoginInfo _loginInfo;
        private readonly IAccountService _accountService;
        private readonly SystemConfigModel _systemConfig;
        public PermissionValidateHandler(IOptionsMonitor<AdminOptions> optionsAccessor, IAccountService accountService, ILoginInfo loginInfo, ISystemService systemService)
        {
            _options = optionsAccessor.CurrentValue;
            _accountService = accountService;
            _loginInfo = loginInfo;
            _systemConfig = systemService.GetConfig().Result.Data;
        }

        public bool Validate(IDictionary<string, string> routeValues, HttpMethod httpMethod)
        {
            if (!_options.PermissionValidate || !_systemConfig.PermissionValidate)
                return true;

            var permissions = _accountService.QueryPermissionList(_loginInfo.AccountId).Result;

            var area = routeValues["area"];
            var controller = routeValues["controller"];
            var action = routeValues["action"];
            return permissions.Any(m => m.ModuleCode.EqualsIgnoreCase(area) && m.Controller.EqualsIgnoreCase(controller) && m.Action.EqualsIgnoreCase(action) && m.HttpMethod == httpMethod);
        }
    }
}
