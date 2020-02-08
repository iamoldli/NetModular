using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NetModular.Lib.Module.AspNetCore
{
    /// <summary>
    /// 模块初始化器接口
    /// </summary>
    public interface IModuleInitializer
    {
        /// <summary>
        /// 注入服务
        /// <para>此方法用于注入与Web相关的服务，否则请通过IModuleServicesConfigurator接口注册</para>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env">环境变量</param>
        void ConfigureServices(IServiceCollection services, IHostEnvironment env);

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
