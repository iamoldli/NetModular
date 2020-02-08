using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Options.Abstraction;
using NetModular.Lib.Utils.Core.Extensions;
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

            #region ==加载系统配置==

            var systemConfig = services.BuildServiceProvider().GetService<SystemConfigResolver>().Load().Result;
            if (systemConfig.Path.UploadPath.IsNull())
            {
                //默认在程序根目录下的Upload目录
                systemConfig.Path.UploadPath = Path.Combine(AppContext.BaseDirectory, "Upload");
            }

            if (systemConfig.Path.TempPath.IsNull())
            {
                //默认在程序根目录下的Temp目录
                systemConfig.Path.TempPath = Path.Combine(AppContext.BaseDirectory, "Temp");
            }

            services.AddSingleton(systemConfig);

            #endregion

            //模块配置项存储处理程序
            services.AddSingleton<IModuleOptionsStorageProvider, ModuleOptionsStorageProvider>();
        }
    }
}
