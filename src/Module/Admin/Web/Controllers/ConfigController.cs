using System.ComponentModel;
using NetModular.Module.Admin.Application.ConfigService;

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
    }
}
