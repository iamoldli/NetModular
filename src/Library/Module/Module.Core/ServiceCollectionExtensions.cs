using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nm.Lib.Module.Abstractions;
using Nm.Lib.Utils.Core;
using Nm.Lib.Utils.Core.Helpers;
using Nm.Lib.Utils.Core.Options;

namespace Nm.Lib.Module.Core
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="services"></param>
        /// <param name="environmentName">环境名称</param>
        /// <returns></returns>
        public static IModuleCollection AddModules(this IServiceCollection services, string environmentName)
        {
            var modules = new ModuleCollection();
            services.AddSingleton<IModuleCollection>(modules);

            var cfgHelper = new ConfigurationHelper();
            var cfg = cfgHelper.Load("module", environmentName, true);

            //通用配置
            services.Configure<ModuleCommonOptions>(cfg);

            foreach (var module in modules)
            {
                if (module == null)
                    continue;

                services.AddApplicationServices(module);

                if (module.Initializer != null)
                {
                    module.Initializer.ConfigureServices(services);

                    module.Initializer.ConfigOptions(services, cfg.GetSection(module.Id));

                    services.AddSingleton(module);
                }
            }

            return modules;
        }

        /// <summary>
        /// 添加应用服务
        /// </summary>
        private static void AddApplicationServices(this IServiceCollection services, ModuleInfo module)
        {
            var types = module.AssembliesInfo.Application.GetTypes();
            var interfaces = types.Where(t => t.FullName != null && t.IsInterface && t.FullName.EndsWith("Service", StringComparison.OrdinalIgnoreCase));
            foreach (var interfaceType in interfaces)
            {
                var implementType = types.FirstOrDefault(m => m != interfaceType && interfaceType.IsAssignableFrom(m));
                if (implementType != null)
                {
                    services.AddScoped(interfaceType, implementType);
                }
            }
        }

        /// <summary>
        /// 自动注入单例服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="module"></param>
        private static void AddSingleton(this IServiceCollection services, ModuleInfo module)
        {
            services.AddSingletonFromAssembly(module.AssembliesInfo.Domain);
            services.AddSingletonFromAssembly(module.AssembliesInfo.Infrastructure);
            services.AddSingletonFromAssembly(module.AssembliesInfo.Application);
            services.AddSingletonFromAssembly(module.AssembliesInfo.Web);
        }
    }
}
