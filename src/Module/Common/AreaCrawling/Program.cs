using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nm.Lib.Host.Generic;
using Nm.Module.Common.Infrastructure.AreaCrawling;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace Nm.Module.Common.AreaCrawling
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new HostBuilder().Run<Startup>(args, ConfigureServices);
        }

        /// <summary>
        /// 注入服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env"></param>
        static void ConfigureServices(IServiceCollection services, IHostingEnvironment env)
        {
            services.AddSingleton<IAreaCrawlingHandler, AreaCrawlingHandler>();
        }
    }
}
