using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Nm.Lib.Auth.Jwt;
using Nm.Lib.Data.AspNetCore;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Lib.Module.Core;
using Nm.Lib.Swagger;
using Nm.Lib.Swagger.Conventions;
using Nm.Lib.Utils.Core;
using Nm.Lib.Utils.Mvc;
using Nm.Lib.Validation.FluentValidation;
using Nm.Lib.WebHost.Core.Options;

namespace Nm.Lib.WebHost.Core
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
        public static IServiceCollection AddWebHost(this IServiceCollection services, HostOptions hostOptions, IHostingEnvironment env)
        {
            services.AddSingleton(hostOptions);

            services.AddUtils();

            services.AddUtilsMvc();

            //加载模块
            var modules = services.AddModules(env);

            //添加对象映射
            services.AddMappers(modules);

            //添加内存缓存
            services.AddMemoryCache();

            //主动或者开发模式下开启Swagger
            if (hostOptions.Swagger || env.IsDevelopment())
            {
                services.AddSwagger(modules);
            }

            //Jwt身份认证
            services.AddJwtAuth(env);

            //添加MVC功能
            services.AddMvc(c =>
            {
                if (env.IsDevelopment())
                {
                    //API分组约定
                    c.Conventions.Add(new ApiExplorerGroupConvention());
                }

                //模块中的MVC配置
                foreach (var module in modules)
                {
                    module.Initializer?.ConfigureMvc(c);
                }

            })
            .AddJsonOptions(options =>
            {
                //设置日期格式化格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            })
            .AddValidators(services)//添加验证器
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //添加数据库
            services.AddDb(env, modules);

            //解决Multipart body length limit 134217728 exceeded
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
            });

            return services;
        }
    }
}
