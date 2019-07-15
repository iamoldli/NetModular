using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nm.Lib.Utils.Core.Options;

namespace Nm.Module.PersonnelFiles.Infrastructure.Options
{
    public class ModuleOptionsConfigure : IModuleOptionsConfigure
    {
        public void ConfigOptions(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PersonnelFilesOptions>(configuration);
        }
    }
}
