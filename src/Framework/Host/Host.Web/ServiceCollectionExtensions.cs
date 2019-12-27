#if NETSTANDARD2_0
using Microsoft.AspNetCore.Hosting;
#endif
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
#if NETCOREAPP3_1
using Microsoft.Extensions.Hosting;
#endif
using NetModular.Lib.Auth.Jwt;
using NetModular.Lib.Cache.Integration;
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
                options.AddPolicy("Default",
                    builder => builder.AllowAnyOrigin()
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

            return services;
        }
    }
}
