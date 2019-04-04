using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.ConfigService.ViewModels;
using NetModular.Module.Admin.Domain.Config;
using NetModular.Module.Admin.Infrastructure.Repositories;

namespace NetModular.Module.Admin.Application.ConfigService
{
    public class ConfigService : IConfigService
    {
        /// <summary>
        /// 系统配置项前缀
        /// </summary>
        private const string SystemConfigPrefix = "sys_";

        private readonly IUnitOfWork _uow;
        private readonly IConfigRepository _configRepository;

        public ConfigService(IConfigRepository configRepository, IUnitOfWork<AdminDbContext> uow)
        {
            _configRepository = configRepository;
            _uow = uow;
        }

        public async Task<IResultModel> GetSystemConfig()
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

            return ResultModel.Success(model);
        }

        public IResultModel UpdateSystemConfig(SystemConfigModel model)
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

    }
}
