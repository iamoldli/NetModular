using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Module.Admin.Web.Core;
using NetModular.Module.Admin.Web.Filters;

namespace NetModular.Module.Admin.Web
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //权限验证服务
            services.AddScoped<IPermissionValidateHandler, PermissionValidateHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var options = app.ApplicationServices.GetService<IOptionsMonitor<AdminOptions>>().CurrentValue;
            UseUpload(app, options);

            //UseWebApp();
        }

        public void ConfigureMvc(MvcOptions mvcOptions)
        {
            mvcOptions.Filters.Add(typeof(AuditingFilter));
        }

        public void ConfigOptions(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AdminOptions>(configuration);
        }

        /// <summary>
        /// 设置模块配置信息访问
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        private void UseWebApp(IApplicationBuilder app)
        {
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
        }

        /// <summary>
        /// 设置文件上传路径
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private void UseUpload(IApplicationBuilder app, AdminOptions options)
        {
            if (options == null)
                options = new AdminOptions();

            if (options.UploadPath.IsNull())
            {
                //默认在程序根目录的Upload目录
                options.UploadPath = Path.Combine(AppContext.BaseDirectory, "Upload/Admin");
            }

            if (!Directory.Exists(options.UploadPath))
            {
                Directory.CreateDirectory(options.UploadPath);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(options.UploadPath),
                RequestPath = "/upload/admin"
            });
        }
    }
}
