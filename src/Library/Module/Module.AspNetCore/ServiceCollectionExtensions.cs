using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Tm.Lib.Module.Abstractions;
using Tm.Lib.Utils.Core;
using Tm.Lib.Utils.Core.Helpers;
using Tm.Lib.Utils.Core.Options;

namespace Tm.Lib.Module.AspNetCore
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
            if (cfg == null)
                return modules;

            //通用配置
            services.Configure<ModuleCommonOptions>(cfg);

            foreach (var module in modules)
            {
                if (module == null)
                    continue;

                services.AddApplicationServices(module);

                //加载模块配置项
                var optionsConfigureType = module.AssemblyDescriptor.Infrastructure.GetTypes().FirstOrDefault(m => typeof(IModuleOptionsConfigure).IsAssignableFrom(m));
                if (optionsConfigureType != null)
                {
                    ((IModuleOptionsConfigure)Activator.CreateInstance(optionsConfigureType)).ConfigOptions(services, cfg.GetSection(module.Id));
                }

                services.AddSingleton(module);
            }

            return modules;
        }

        /// <summary>
        /// 添加模块的自定义服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public static IServiceCollection AddModuleServices(this IServiceCollection services, IModuleCollection modules)
        {
            foreach (var module in modules)
            {
                //加载模块初始化器
                ((ModuleDescriptor) module)?.Initializer.ConfigureServices(services);
            }

            return services;
        }

        /// <summary>
        /// 添加应用服务
        /// </summary>
        private static void AddApplicationServices(this IServiceCollection services, IModuleDescriptor module)
        {
            if (module.AssemblyDescriptor == null)
                return;

            var types = module.AssemblyDescriptor.Application.GetTypes();
            var interfaces = types.Where(t => t.FullName != null && t.IsInterface && t.FullName.EndsWith("Service", StringComparison.OrdinalIgnoreCase));
            foreach (var serviceType in interfaces)
            {
                var implementationType = types.FirstOrDefault(m => m != serviceType && serviceType.IsAssignableFrom(m));
                if (implementationType != null)
                {
                    services.Add(new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Singleton));
                }
            }
        }

        /// <summary>
        /// 自动注入单例服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="module"></param>
        private static void AddSingleton(this IServiceCollection services, IModuleDescriptor module)
        {
            if (module.AssemblyDescriptor != null && module.AssemblyDescriptor is ModuleAssemblyDescriptor descriptor)
            {
                services.AddSingletonFromAssembly(descriptor.Domain);
                services.AddSingletonFromAssembly(descriptor.Infrastructure);
                services.AddSingletonFromAssembly(descriptor.Application);
                services.AddSingletonFromAssembly(descriptor.Web);
            }
        }
    }
}
