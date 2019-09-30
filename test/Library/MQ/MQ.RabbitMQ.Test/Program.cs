using Microsoft.Extensions.DependencyInjection;
using Nm.Lib.MQ.RabbitMQ;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace MQ.RabbitMQ.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger();
            logger.Debug("测试");
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

    public class Logger
    {
        public void Debug(string msg)
        {
            //写入文件
        }
    }
}
