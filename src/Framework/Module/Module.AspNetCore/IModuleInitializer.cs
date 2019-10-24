using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Nm.Lib.Module.AspNetCore
{
    /// <summary>
    /// 模块初始化器接口
    /// </summary>
    public interface IModuleInitializer
    {
        /// <summary>
        /// 注入服务
        /// </summary>
        /// <param name="services"></param>
        void ConfigureServices(IServiceCollection services);

        /// <summary>
        /// 配置中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        void Configure(IApplicationBuilder app, IHostEnvironment env);

        /// <summary>
        /// 配置MVC
        /// </summary>
        /// <param name="mvcOptions"></param>
        void ConfigureMvc(MvcOptions mvcOptions);
    }
}
