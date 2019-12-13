using Microsoft.AspNetCore.Builder;
#if NETSTANDARD2_0
using Microsoft.AspNetCore.Hosting;
#endif
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
#if NETCOREAPP3_1
using Microsoft.Extensions.Hosting;
#endif
using Microsoft.Extensions.Options;
using NetModular.Lib.Utils.Core.Options;
using NetModular.Module.Admin.Web.Core;
using NetModular.Module.Admin.Web.Filters;
using NetModular.Lib.Auth.Web;
using NetModular.Lib.Module.AspNetCore;
using System.IO;
using NetModular.Module.Admin.Application.SystemService;

namespace NetModular.Module.Admin.Web
{
    public class ModuleInitializer : IModuleInitializer
    {
#if NETSTANDARD2_0
        public void ConfigureServices(IServiceCollection services, IHostingEnvironment env)
#elif NETCOREAPP3_1
        public void ConfigureServices(IServiceCollection services, IHostEnvironment env)
#endif
        {
            //审计日志服务
            services.AddSingleton<IAuditingHandler, AuditingHandler>();
            //权限验证服务
            services.AddScoped<IPermissionValidateHandler, PermissionValidateHandler>();

            //注入系统配置
            var systemConfig = services.BuildServiceProvider().GetService<SystemConfigResolver>().GetConfig().Result;
            services.AddSingleton(systemConfig);
        }

#if NETSTANDARD2_0
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
#elif NETCOREAPP3_1
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
#endif
        {
            var options = app.ApplicationServices.GetService<IOptionsMonitor<ModuleCommonOptions>>().CurrentValue;

            var logoPath = Path.Combine(options.UploadPath, "Admin/Logo");
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
