using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
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
        /// <param name="env"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWebHost(this IApplicationBuilder app, IHostingEnvironment env)
        {
            //主机配置信息
            var hostOptions = app.ApplicationServices.GetService<HostOptions>();

            //异常处理
            app.UseExceptionHandle();

            //加载模块
            app.UseModules(env);

            //启用静态文件
            app.UseStaticFiles();

            //允许访问模块前端代码
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(AppContext.BaseDirectory, "Modules")),
                RequestPath = "/modules",
                OnPrepareResponse = ctx =>
                {
                    //禁止访问注释文档以及模块信息文件
                    if (ctx.File.Name.Equals("doc.xml") || ctx.File.Name.Equals("module.json"))
                    {
                        ctx.Context.Response.StatusCode = 404;
                    }
                }
            });

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
