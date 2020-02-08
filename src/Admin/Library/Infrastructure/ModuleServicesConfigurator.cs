using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Options.Abstraction;
using NetModular.Module.Admin.Infrastructure.PasswordHandler;

namespace NetModular.Module.Admin.Infrastructure
{
    /// <summary>
    /// 模块服务配置器
    /// </summary>
    public class ModuleServicesConfigurator : IModuleServicesConfigurator
    {
        public void Configure(IServiceCollection services, IHostEnvironment env)
        {
            //密码处理服务
            services.AddSingleton<IPasswordHandler, Md5PasswordHandler>();

            //加载系统配置
            var systemConfig = services.BuildServiceProvider().GetService<SystemConfigResolver>().Load().Result;
            services.AddSingleton(systemConfig);

            //模块配置项存储处理程序
            services.AddSingleton<IModuleOptionsStorageProvider, ModuleOptionsStorageProvider>();
        }
    }
}
