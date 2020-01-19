#if NETSTANDARD2_0
using Microsoft.AspNetCore.Hosting;
#endif
using System;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
#if NETCOREAPP3_1
using Microsoft.Extensions.Hosting;
#endif
using NetModular.Lib.Auth.Jwt;
using NetModular.Lib.Cache.Integration;
using NetModular.Lib.Config.Core;
using NetModular.Lib.Data.Integration;
using NetModular.Lib.Excel.Integration;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Lib.Module.AspNetCore;
using NetModular.Lib.Swagger.Core;
using NetModular.Lib.Swagger.Core.Conventions;
using NetModular.Lib.Utils.Core;
using NetModular.Lib.Utils.Core.Options;
using NetModular.Lib.Utils.Mvc;
using NetModular.Lib.Validation.FluentValidation;
using HostOptions = NetModular.Lib.Host.Web.Options.HostOptions;

namespace NetModular.Lib.Host.Web
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加WebHost
        /// </summary>
        /// <param name="services"></param>
        /// <param name="hostOptions"></param>
        /// <param name="env">环境</param>
        /// <returns></returns>
#if NETSTANDARD2_0
        public static IServiceCollection AddWebHost(this IServiceCollection services, HostOptions hostOptions, IHostingEnvironment env)
#elif NETCOREAPP3_1
        public static IServiceCollection AddWebHost(this IServiceCollection services, HostOptions hostOptions, IHostEnvironment env)
#endif
        {
            services.AddSingleton(hostOptions);

            services.AddUtils();

            services.AddUtilsMvc();

            //加载模块
            var modules = services.AddModules(env.EnvironmentName, out ModuleCommonOptions moduleCommonOptions);

            //添加对象映射
            services.AddMappers(modules);

            //添加缓存
            services.AddCache(env.EnvironmentName);

            //添加Excel相关功能
            services.AddExcel(env.EnvironmentName, moduleCommonOptions);

            //主动或者开发模式下开启Swagger
            if (hostOptions.Swagger || env.IsDevelopment())
            {
                services.AddSwagger(modules);
            }

            //Jwt身份认证
            services.AddJwtAuth(env.EnvironmentName);

            //添加MVC功能
            services.AddMvc(c =>
            {
                if (hostOptions.Swagger || env.IsDevelopment())
                {
                    //API分组约定
                    c.Conventions.Add(new ApiExplorerGroupConvention());
                }

                //模块中的MVC配置
                foreach (var module in modules)
                {
                    ((ModuleDescriptor)module).Initializer?.ConfigureMvc(c);
                }

            })
#if NETSTANDARD2_0
            .AddJsonOptions(options =>
#elif NETCOREAPP3_1
            .AddNewtonsoftJson(options =>
#endif
            {
                //设置日期格式化格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            })
            .AddValidators(services)//添加验证器
#if NETSTANDARD2_0
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
#elif NETCOREAPP3_1
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
#endif

            //CORS
            services.AddCors(options =>
            {
                /*浏览器的同源策略，就是出于安全考虑，浏览器会限制从脚本发起的跨域HTTP请求（比如异步请求GET, POST, PUT, DELETE, OPTIONS等等，
                        所以浏览器会向所请求的服务器发起两次请求，第一次是浏览器使用OPTIONS方法发起一个预检请求，第二次才是真正的异步请求，
                        第一次的预检请求获知服务器是否允许该跨域请求：如果允许，才发起第二次真实的请求；如果不允许，则拦截第二次请求。
                        Access-Control-Max-Age用来指定本次预检请求的有效期，单位为秒，，在此期间不用发出另一条预检请求。*/
                var preflightMaxAge = hostOptions.PreflightMaxAge < 0 ? new TimeSpan(0, 30, 0) : new TimeSpan(0, 0, hostOptions.PreflightMaxAge);

                options.AddPolicy("Default",
                    builder => builder.AllowAnyOrigin()
                        .SetPreflightMaxAge(preflightMaxAge)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Content-Disposition"));//下载文件时，文件名称会保存在headers的Content-Disposition属性里面
            });

            //添加数据库，数据库依赖ILoginInfo，所以需要在添加身份认证以及MVC后添加数据库
            services.AddDb(env.EnvironmentName, modules);

            //解决Multipart body length limit 134217728 exceeded
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
            });

            //添加HttpClient相关
            services.AddHttpClient();

            //添加模块的自定义服务
            services.AddModuleServices(modules, env);

            //添加配置管理
            services.AddConfig();

            return services;
        }
    }
}
