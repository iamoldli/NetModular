using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using NetModular.Lib.Module.Core;
using NetModular.Lib.Swagger;
using NetModular.Lib.WebHost.Core.Middlewares;
using NetModular.Lib.WebHost.Core.Options;

namespace NetModular.Lib.WebHost.Core
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 启用WebHost
        /// </summary>
        /// <param name="app"></param>
        /// <param name="hostOptions"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWebHost(this IApplicationBuilder app, HostOptions hostOptions, IHostingEnvironment env)
        {
            //异常处理
            app.UseExceptionHandle();

            //加载模块
            app.UseModules(env);

            //启用静态文件
            app.UseStaticFiles();

            //身份认证
            app.UseAuthentication();

            //CORS
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
                builder.AllowCredentials();
            });

            //开启Swagger
            if (hostOptions.Swagger || env.IsDevelopment())
            {
                app.UseCustomSwagger();
            }

            app.UseMvc();

            return app;
        }
    }
}
