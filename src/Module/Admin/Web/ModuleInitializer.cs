﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Tm.Lib.Utils.Core.Options;
using Tm.Module.Admin.Web.Core;
using Tm.Module.Admin.Web.Filters;
using System.IO;
using Tm.Lib.Auth.Web;
using Tm.Lib.Module.AspNetCore;

namespace Tm.Module.Admin.Web
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
