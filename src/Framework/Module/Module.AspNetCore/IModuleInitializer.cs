using Microsoft.AspNetCore.Builder;
#if NETSTANDARD2_0
using Microsoft.AspNetCore.Hosting;
#endif
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
#if NETCOREAPP3_1
using Microsoft.Extensions.Hosting;
#endif

namespace NetModular.Lib.Module.AspNetCore
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
        /// <param name="env">环境变量</param>
#if NETSTANDARD2_0
        void ConfigureServices(IServiceCollection services, IHostingEnvironment env);
#elif NETCOREAPP3_1
        void ConfigureServices(IServiceCollection services, IHostEnvironment env);
#endif

        /// <summary>
        /// 配置中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
#if NETSTANDARD2_0
        void Configure(IApplicationBuilder app, IHostingEnvironment env);
#elif NETCOREAPP3_1
        void Configure(IApplicationBuilder app, IHostEnvironment env);
#endif

        /// <summary>
        /// 配置MVC
        /// </summary>
        /// <param name="mvcOptions"></param>
        void ConfigureMvc(MvcOptions mvcOptions);
    }
}
