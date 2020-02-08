using System;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Options.Abstraction;

namespace NetModular.Lib.Options.Core
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加模块配置信息
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddModuleOptions(this IServiceCollection services)
        {
            services.AddSingleton<IModuleOptionsContainer, ModuleOptionsContainer>();

            //启动时加载配置信息
            var container = services.BuildServiceProvider().GetService<IModuleOptionsContainer>();
            container.Load(services);

            return services;
        }
    }
}
