using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetModular.Lib.Module.Abstractions;

namespace NetModular.Module.Admin.Infrastructure
{
    /// <summary>
    /// 模块服务配置器
    /// </summary>
    public class ModuleServicesConfigurator : IModuleServicesConfigurator
    {
        public void Configure(IServiceCollection services, IModuleCollection modules, IHostEnvironment env, IConfiguration cfg)
        {
        }
    }
}
