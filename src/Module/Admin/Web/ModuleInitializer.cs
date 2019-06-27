using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Module.Abstractions;
using Nm.Lib.Utils.Core.Options;
using Nm.Module.Admin.Infrastructure.Options;
using Nm.Module.Admin.Web.Core;
using Nm.Module.Admin.Web.Filters;
using System.IO;

namespace Nm.Module.Admin.Web
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
            var options = app.ApplicationServices.GetService<IOptionsMonitor<ModuleCommonOptions>>().CurrentValue;

            var logoPath = Path.Combine(options.UploadPath, "admin/logo");
            if (!Directory.Exists(logoPath))
            {
                Directory.CreateDirectory(logoPath);
            }
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

        public void ConfigOptions(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AdminOptions>(configuration);
        }
    }
}
