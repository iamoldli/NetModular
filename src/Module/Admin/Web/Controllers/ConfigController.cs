using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.ConfigService;
using NetModular.Module.Admin.Application.ConfigService.ViewModels;
using NetModular.Module.Admin.Web.Attributes;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("配置项管理")]
    public class ConfigController : ModuleController
    {
        private readonly IConfigService _configService;

        public ConfigController(IConfigService configService)
        {
            _configService = configService;
        }

        [HttpGet]
        [AllowAnonymous]
        [DisableAuditing]
        [Description("获取系统配置信息")]
        public Task<IResultModel> SystemConfig()
        {
            return _configService.GetSystemConfig();
        }

        [HttpPost]
        [Description("修改系统配置")]
        public IResultModel SystemConfigUpdate(SystemConfigModel model)
        {
            return _configService.UpdateSystemConfig(model);
        }
    }
}
