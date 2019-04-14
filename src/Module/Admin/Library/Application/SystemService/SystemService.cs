using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.AccountService;
using NetModular.Module.Admin.Application.AccountService.ViewModels;
using NetModular.Module.Admin.Application.ModuleInfoService;
using NetModular.Module.Admin.Application.PermissionService;
using NetModular.Module.Admin.Application.SystemService.ViewModels;
using NetModular.Module.Admin.Domain.Config;
using NetModular.Module.Admin.Domain.Role;
using NetModular.Module.Admin.Infrastructure.Repositories;

namespace NetModular.Module.Admin.Application.SystemService
{
    public class SystemService : ISystemService
    {
        /// <summary>
        /// 系统配置项前缀
        /// </summary>
        private const string SystemConfigPrefix = "sys_";

        private readonly IUnitOfWork _uow;
        private readonly IConfigRepository _configRepository;
        private readonly IModuleInfoService _moduleInfoService;
        private readonly IPermissionService _permissionService;
        private readonly IAccountService _accountService;
        private readonly IRoleRepository _roleRepository;
        public SystemService(IUnitOfWork<AdminDbContext> uow, IConfigRepository configRepository, IModuleInfoService moduleInfoService, IPermissionService permissionService, IAccountService accountService, IRoleRepository roleRepository)
        {
            _uow = uow;
            _configRepository = configRepository;
            _moduleInfoService = moduleInfoService;
            _permissionService = permissionService;
            _accountService = accountService;
            _roleRepository = roleRepository;
        }

        public async Task<IResultModel> GetConfig(string host)
        {
            var model = new SystemConfigModel();

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
                    case SystemConfigKey.ButtonPermission:
                        model.ButtonPermission = config.Value.ToBool();
                        break;
                    case SystemConfigKey.Auditing:
                        model.Auditing = config.Value.ToBool();
                        break;
                    case SystemConfigKey.ToolbarFullscreen:
                        model.Toolbar.Fullscreen = config.Value.ToBool();
                        break;
                    case SystemConfigKey.ToolbarSkin:
                        model.Toolbar.Skin = config.Value.ToBool();
                        break;
                }
            }

            if (model.Logo.NotNull())
            {
                model.LogoUrl = new Uri($"{host}/upload/admin/{model.Logo}").ToString().ToLower();
            }

            return ResultModel.Success(model);
        }

        public IResultModel UpdateConfig(SystemConfigModel model)
        {
            if (model == null)
                return ResultModel.Failed();

            var tasks = new List<Task>();

            _uow.BeginTransaction();

            tasks.Add(_configRepository.UpdateAsync(new Config
            {
                Key = SystemConfigKey.Title,
                Value = model.Title,
                Remarks = "系统标题"
            }));
            tasks.Add(_configRepository.UpdateAsync(new Config
            {
                Key = SystemConfigKey.Logo,
                Value = model.Logo,
                Remarks = "系统Logo"
            }));
            tasks.Add(_configRepository.UpdateAsync(new Config
            {
                Key = SystemConfigKey.Home,
                Value = model.Home,
                Remarks = "系统首页"
            }));
            tasks.Add(_configRepository.UpdateAsync(new Config
            {
                Key = SystemConfigKey.ButtonPermission,
                Value = model.ButtonPermission.ToString(),
                Remarks = "启用按钮权限"
            }));
            tasks.Add(_configRepository.UpdateAsync(new Config
            {
                Key = SystemConfigKey.Auditing,
                Value = model.Auditing.ToString(),
                Remarks = "启用审计日志"
            }));
            tasks.Add(_configRepository.UpdateAsync(new Config
            {
                Key = SystemConfigKey.ToolbarFullscreen,
                Value = model.Toolbar.Fullscreen.ToString(),
                Remarks = "显示工具栏全屏按钮"
            }));
            tasks.Add(_configRepository.UpdateAsync(new Config
            {
                Key = SystemConfigKey.ToolbarSkin,
                Value = model.Toolbar.Skin.ToString(),
                Remarks = "显示工具栏皮肤按钮"
            }));

            Task.WaitAll(tasks.ToArray());

            _uow.Commit();

            return ResultModel.Success();
        }

        public async Task<IResultModel> Install(SystemInstallModel model)
        {
            await _moduleInfoService.Sync();
            await _permissionService.Sync(model.Permissions);

            var role = new Role
            {
                Name = "系统管理员"
            };

            await _roleRepository.AddAsync(role);
            await _accountService.Add(new AccountAddModel
            {
                UserName = "admin",
                Password = "admin",
                Name = "管理员",
                Roles = new List<Guid> { role.Id }
            });

            UpdateConfig(new SystemConfigModel
            {
                Title = "通用权限管理系统",
                Auditing = false,
                Toolbar = new SystemToolbar
                {
                    Fullscreen = true,
                    Skin = true
                }
            });

            return ResultModel.Success();
        }
    }
}
