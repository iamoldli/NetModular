using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Module.Abstractions;

namespace NetModular.Lib.Module.Core
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IModuleCollection AddModules(this IServiceCollection services)
        {
            var modules = new ModuleCollection();
            services.AddSingleton<IModuleCollection>(modules);

            foreach (var module in modules)
            {
                services.AddApplicationServices(module);

                module.Initializer?.ConfigureServices(services);
            }

            return modules;
        }

        /// <summary>
        /// 添加应用服务
        /// </summary>
        private static void AddApplicationServices(this IServiceCollection services, ModuleInfo module)
        {
            if (module == null)
                return;

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
    }
}
