using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Auth.Jwt;
using NetModular.Lib.Data.AspNetCore;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Lib.Module.Core;
using NetModular.Lib.Swagger;
using NetModular.Lib.Swagger.Conventions;
using NetModular.Lib.Validation.FluentValidation;
using NetModular.Lib.WebHost.Core.Options;
using ConfigurationExtensions = NetModular.Lib.Utils.Core.Extensions.ConfigurationExtensions;

namespace NetModular.Lib.WebHost.Core
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加WebHost
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cfg">配置项</param>
        /// <param name="env">环境</param>
        /// <param name="hostSetupAction">修改主机配置</param>
        /// <returns></returns>
        public static IServiceCollection AddWebHost(this IServiceCollection services, IConfiguration cfg, IHostingEnvironment env, Action<HostOptions> hostSetupAction = null)
        {
            //加载主机配置项
            var hostOptions = ConfigurationExtensions.Get<HostOptions>("Host");

            hostSetupAction?.Invoke(hostOptions);
            services.AddSingleton(hostOptions);

            //加载模块
            var modules = services.AddModules();

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

            return services;
        }
    }
}
