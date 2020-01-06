using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Config.Abstraction;

namespace NetModular.Lib.Config.Core
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加配置管理
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddConfig(this IServiceCollection services)
        {
            services.AddSingleton<IConfigContainer, ConfigContainer>();

            //启动时执行一次刷新
            var container = services.BuildServiceProvider().GetService<IConfigContainer>();
            container.RefreshAll().GetAwaiter().GetResult();

            return services;
        }
    }
}
