using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Config.Abstractions;

namespace NetModular.Lib.Config.Redis
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加配置管理(使用Redis)
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddConfigWithRedis(this IServiceCollection services)
        {
            //配置核心服务
            services.AddConfigCore();

            //配置实例提供器
            services.AddSingleton<IConfigProvider, ConfigProvider>();

            return services;
        }
    }
}
