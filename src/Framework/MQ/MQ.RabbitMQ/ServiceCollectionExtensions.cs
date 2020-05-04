using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetModular.Lib.MQ.RabbitMQ
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加RabbitMQ功能
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration cfg)
        {
            var config = new RabbitMQConfig();
            var section = cfg.GetSection("RabbitMQ");
            section?.Bind(config);

            services.AddSingleton(config);
            services.AddSingleton<RabbitMQClient>();

            return services;
        }
    }
}