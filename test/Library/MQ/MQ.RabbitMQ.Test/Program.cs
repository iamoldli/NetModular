using System;
using Microsoft.Extensions.DependencyInjection;
using Nm.Lib.Host.Generic;
using Nm.Lib.MQ.RabbitMQ;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace MQ.RabbitMQ.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            new HostBuilder().Run<Startup>(args, ConfigureServices);

            Console.Read();
        }

        /// <summary>
        /// 注入服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env"></param>
        static void ConfigureServices(IServiceCollection services, IHostingEnvironment env)
        {
            services.AddRabbitMQ(env.EnvironmentName);
        }
    }
}
