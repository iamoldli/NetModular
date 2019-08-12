using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Tm.Lib.Cache.Abstractions;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.Admin.Application.ModuleInfoService;
using Tm.Module.Admin.Application.PermissionService;
using Tm.Module.Admin.Application.SystemService.ViewModels;
using Tm.Module.Admin.Domain.Account;
using Tm.Module.Admin.Domain.AccountRole;
using Tm.Module.Admin.Domain.Config;
using Tm.Module.Admin.Domain.Role;

namespace Tm.Module.Admin.Application.SystemService
{
    public class SystemService : ISystemService
    {
        /// <summary>
        /// 系统配置项前缀
        /// </summary>
        private const string SystemConfigPrefix = "sys_";

        private const string SystemConfigCacheKey = "ADMIN_SYSTEM_CONFIG";

        private readonly IConfigRepository _configRepository;
        private readonly IModuleInfoService _moduleInfoService;
        private readonly IPermissionService _permissionService;
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly ICacheHandler _cache;

        public SystemService(IConfigRepository configRepository, IModuleInfoService moduleInfoService, IPermissionService permissionService, IRoleRepository roleRepository, ICacheHandler cache, IAccountRepository accountRepository, IAccountRoleRepository accountRoleRepository)
        {
            _configRepository = configRepository;
            _moduleInfoService = moduleInfoService;
            _permissionService = permissionService;
            _roleRepository = roleRepository;
            _cache = cache;
            _accountRepository = accountRepository;
            _accountRoleRepository = accountRoleRepository;
        }

        public async Task<IResultModel<SystemConfigModel>> GetConfig(string host = null)
        {
            var result = new ResultModel<SystemConfigModel>();
            if (!_cache.TryGetValue(SystemConfigCacheKey, out SystemConfigModel model))
            {
                model = new SystemConfigModel();

                var configList = await _configRepository.QueryByPrefix(SystemConfigPrefix);

                foreach (var config in configList)
                {
                    switch (config.Key)
                    {
                        case SystemConfigKey.Title:
                            model.Title = config.Value;
                            break;
                        case SystemConfigKey.Logo:
                            model.Logo = config.Value;
                            break;
                        case SystemConfigKey.Home:
                            model.Home = config.Value;
                            break;
                        case SystemConfigKey.UserInfoPage:
                            model.UserInfoPage = config.Value;
                            break;
                        case SystemConfigKey.ButtonPermission:
                            model.ButtonPermission = config.Value.ToBool();
                            break;
                        case SystemConfigKey.PermissionValidate:
                            model.PermissionValidate = config.Value.ToBool();
                            break;
                        case SystemConfigKey.Auditing:
                            model.Auditing = config.Value.ToBool();
                            break;
                        case SystemConfigKey.LoginVerifyCode:
                            model.LoginVerifyCode = config.Value.ToBool();
                            break;
                        case SystemConfigKey.ToolbarFullscreen:
                            model.Toolbar.Fullscreen = config.Value.ToBool();
                            break;
                        case SystemConfigKey.ToolbarSkin:
                            model.Toolbar.Skin = config.Value.ToBool();
                            break;
                        case SystemConfigKey.ToolbarLogout:
                            model.Toolbar.Logout = config.Value.ToBool();
                            break;
                        case SystemConfigKey.ToolbarUserInfo:
                            model.Toolbar.UserInfo = config.Value.ToBool();
                            break;
                        case SystemConfigKey.CustomCss:
                            model.CustomCss = config.Value;
                            break;
                    }
                }

                await _cache.SetAsync(SystemConfigCacheKey, model);
            }

            if (host.NotNull() && model.Logo.NotNull())
            {
                model.LogoUrl = new Uri($"{host}/upload/{model.Logo}").ToString().ToLower();
            }

            return result.Success(model);
        }

        public IResultModel UpdateConfig(SystemConfigModel model)
        {
            if (model == null)
                return ResultModel.Failed();

            using (var tran = _configRepository.BeginTransaction())
            {
                Update(SystemConfigKey.Title, model.Title, "系统标题", tran).Wait();
                Update(SystemConfigKey.Logo, model.Logo, "系统Logo", tran).Wait();
                Update(SystemConfigKey.Home, model.Home, "系统首页", tran).Wait();
                Update(SystemConfigKey.UserInfoPage, model.UserInfoPage, "个人信息页", tran).Wait();
                Update(SystemConfigKey.ButtonPermission, model.ButtonPermission, "启用按钮权限", tran).Wait();
                Update(SystemConfigKey.PermissionValidate, model.PermissionValidate, "启用权限验证功能", tran).Wait();
                Update(SystemConfigKey.Auditing, model.Auditing, "启用审计日志", tran).Wait();
                Update(SystemConfigKey.LoginVerifyCode, model.LoginVerifyCode, "启用登录验证码功能", tran).Wait();
                Update(SystemConfigKey.ToolbarFullscreen, model.Toolbar.Fullscreen, "显示工具栏全屏按钮", tran).Wait();
                Update(SystemConfigKey.ToolbarSkin, model.Toolbar.Skin, "显示工具栏皮肤按钮", tran).Wait();
                Update(SystemConfigKey.ToolbarLogout, model.Toolbar.Logout, "显示工具栏退出按钮", tran).Wait();
                Update(SystemConfigKey.ToolbarUserInfo, model.Toolbar.UserInfo, "显示工具栏用户信息按钮", tran).Wait();
                Update(SystemConfigKey.CustomCss, model.CustomCss, "自定义CSS样式", tran).Wait();

                tran.Commit();
            }

            _cache.RemoveAsync(SystemConfigCacheKey).Wait();

            return ResultModel.Success();
        }

        private Task<bool> Update(string key, object value, string remarks, IDbTransaction tran)
        {
            return _configRepository.UpdateAsync(new ConfigEntity
            {
                Key = key,
                Value = value.ToString(),
                Remarks = remarks
            }, tran);
        }

        public async Task<IResultModel> Install(SystemInstallModel model)
        {
            await _moduleInfoService.Sync();
            await _permissionService.Sync(model.Permissions);

            var role = new RoleEntity
            {
                Name = "系统管理员"
            };

            await _roleRepository.AddAsync(role);

            var account = new AccountEntity
            {
                UserName = "tdkj",
                Password = "FDFAEC6B4F80E739A50ADC802C5B537C",
                Name = "管理员"
            };
            await _accountRepository.AddAsync(account);

            await _accountRoleRepository.AddAsync(new AccountRoleEntity { AccountId = account.Id, RoleId = role.Id });

            UpdateConfig(new SystemConfigModel
            {
                Title = "腾迪权限管理系统",
                Auditing = false,
                Toolbar = new SystemToolbar
                {
                    Fullscreen = true,
                    Skin = true,
                    Logout = true,
                    UserInfo = true
                }
            });

            return ResultModel.Success();
        }
    }
}
