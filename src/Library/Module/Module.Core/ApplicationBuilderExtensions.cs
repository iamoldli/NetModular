using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Module.Abstractions;

namespace NetModular.Lib.Module.Core
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseModules(this IApplicationBuilder app, IHostingEnvironment env)
        {
            var modules = app.ApplicationServices.GetService<IModuleCollection>();
            foreach (var module in modules)
            {
                module.Initializer?.Configure(app, env);
            }

            //解析模块信息
            app.Use(async (context, next) =>
            {
                var moduleId = context.Request.Path.Value.Split('/')[1];
                var module = modules.SingleOrDefault(m => m.Id.Equals(moduleId, StringComparison.OrdinalIgnoreCase));
                if (module != null)
                {
                    context.Items.Add("Module", module);
                }

                await next.Invoke();
            });

            return app;
        }
    }
}
