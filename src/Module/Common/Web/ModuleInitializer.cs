using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nm.Lib.Module.Abstractions;
using Nm.Module.Common.Infrastructure.AreaCrawling;
using Nm.Module.Common.Infrastructure.Options;

namespace Nm.Module.Common.Web
{
    public class ModuleInitializer : IModuleInitializer
    {
        /// <summary>
        /// 注入服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAreaCrawlingHandler, AreaCrawlingHandler>();
        }

        /// <summary>
        /// 配置中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        }

        /// <summary>
        /// 配置MVC功能
        /// </summary>
        /// <param name="mvcOptions"></param>
        public void ConfigureMvc(MvcOptions mvcOptions)
        {

        }

        /// <summary>
        /// 配置配置项
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public void ConfigOptions(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CommonOptions>(configuration);
        }
    }
}
