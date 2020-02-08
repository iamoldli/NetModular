using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using NetModular.Module.Admin.Web.Core;
using NetModular.Module.Admin.Web.Filters;
using NetModular.Lib.Auth.Web;
using NetModular.Lib.Module.AspNetCore;
using System.IO;
using NetModular.Lib.Utils.Core.SystemConfig;

namespace NetModular.Module.Admin.Web
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection services, IHostEnvironment env)
        {
            //审计日志服务
            services.AddSingleton<IAuditingHandler, AuditingHandler>();
            //权限验证服务
            services.AddScoped<IPermissionValidateHandler, PermissionValidateHandler>();
            //单账户登录处理服务
            services.AddScoped<ISingleAccountLoginHandler, SingleAccountLoginHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            var options = app.ApplicationServices.GetService<SystemConfigModel>();

            var logoPath = Path.Combine(options.Path.UploadPath, "Admin/Logo");
            if (!Directory.Exists(logoPath))
            {
                Directory.CreateDirectory(logoPath);
            }

            //开放logo访问权限
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(logoPath),
                RequestPath = "/upload/admin/logo"
            });
        }

        public void ConfigureMvc(MvcOptions mvcOptions)
        {
            mvcOptions.Filters.Add(typeof(AuditingFilter));
        }
    }
}
