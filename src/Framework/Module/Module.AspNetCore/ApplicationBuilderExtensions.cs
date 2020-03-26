using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetModular.Lib.Module.Abstractions;

namespace NetModular.Lib.Module.AspNetCore
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseModules(this IApplicationBuilder app, IHostEnvironment env)
        {
            var modules = app.ApplicationServices.GetService<IModuleCollection>();
            foreach (var module in modules)
            {
                //加载模块初始化器
                ((ModuleDescriptor)module).Initializer?.Configure(app, env);
            }

            //解析模块信息
            //app.Use(async (context, next) =>
            //{
            //    var moduleId = context.Request.Path.Value.Split('/')[1];
            //    var module = modules.SingleOrDefault(m => m.Id.Equals(moduleId, StringComparison.OrdinalIgnoreCase));
            //    if (module != null)
            //    {
            //        context.Items.Add("Module", module);
            //    }

            //    await next.Invoke();
            //});

            return app;
        }
    }
}
