using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Utils.Core.Options;

namespace NetModular.Module.Admin.Infrastructure.Options
{
    public class ModuleOptionsConfigure : IModuleOptionsConfigure
    {
        public void ConfigOptions(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AdminOptions>(configuration);
        }
    }
}
