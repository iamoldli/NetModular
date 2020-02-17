using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Utils.Core.Helpers;

namespace NetModular.Lib.MQ.RabbitMQ
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加RabbitMQ
        /// </summary>
        /// <param name="services"></param>
        /// <param name="environmentName"></param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, string environmentName)
        {
            var cfgHelper = new ConfigurationHelper();
            var options = cfgHelper.Get<RabbitMQOptions>("MQ", environmentName);
            if (options == null)
                return services;

            services.AddSingleton(options);
            services.AddSingleton<RabbitMQClient>();

            return services;
        }
    }
}
