using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
#if NETSTANDARD2_0
using Microsoft.AspNetCore.Hosting;
#endif
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
#if NETCOREAPP3_0
using Microsoft.Extensions.Hosting;
#endif
using NetModular.Lib.Host.Web.Middleware;
using NetModular.Lib.Module.AspNetCore;
using NetModular.Lib.Swagger.Core;
using NetModular.Lib.Utils.Core.Interfaces;
using HostOptions = NetModular.Lib.Host.Web.Options.HostOptions;

namespace NetModular.Lib.Host.Web
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
#if NETSTANDARD2_0
        public static IApplicationBuilder UseWebHost(this IApplicationBuilder app, HostOptions hostOptions, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
#elif NETCOREAPP3_0
        public static IApplicationBuilder UseWebHost(this IApplicationBuilder app, HostOptions hostOptions, IHostEnvironment env)
#endif
        {
            //异常处理
            app.UseExceptionHandle();

            //加载模块
            app.UseModules(env);

            //设置默认文档
            var defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(defaultFilesOptions);

            app.UseDefaultPage();

            app.UseDocs();

            //反向代理
            if (hostOptions.Proxy)
            {
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }

#if NETCOREAPP3_0
            //路由
            app.UseRouting();
#endif

            //CORS
            app.UseCors("Default");

            //认证
            app.UseAuthentication();

#if NETCOREAPP3_0
            //授权
            app.UseAuthorization();

            //配置端点
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
#endif

            //开启Swagger
            if (hostOptions.Swagger || env.IsDevelopment())
            {
                app.UseCustomSwagger();
            }

#if NETSTANDARD2_0
            app.UseMvc();
#endif

            return app;
        }

        /// <summary>
        /// 启用默认页
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseDefaultPage(this IApplicationBuilder app)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/app");
            if (Directory.Exists(path))
            {
                var options = new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(path),
                    RequestPath = new PathString("/app")
                };

                app.UseStaticFiles(options);

                var rewriteOptions = new RewriteOptions().AddRedirect("^$", "app");

                app.UseRewriter(rewriteOptions);
            }

            return app;
        }

        /// <summary>
        /// 启动文档页
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseDocs(this IApplicationBuilder app)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/docs");
            if (Directory.Exists(path))
            {
                var options = new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(path),
                    RequestPath = new PathString("/docs")
                };

                app.UseStaticFiles(options);
            }

            return app;
        }

        /// <summary>
        /// 启用应用停止处理
        /// </summary>
        /// <returns></returns>
        public static IApplicationBuilder UseShutdownHandler(this IApplicationBuilder app)
        {
#if NETSTANDARD2_0
            var applicationLifetime = app.ApplicationServices.GetRequiredService<Microsoft.AspNetCore.Hosting.IApplicationLifetime>();
#elif NETCOREAPP3_0
            var applicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
#endif
            applicationLifetime.ApplicationStopping.Register(() =>
            {
                var handlers = app.ApplicationServices.GetServices<IAppShutdownHandler>().ToList();
                foreach (var handler in handlers)
                {
                    handler.Handle();
                }
            });

            return app;
        }
    }
}
