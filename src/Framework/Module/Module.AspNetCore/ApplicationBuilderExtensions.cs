using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
#if NETSTANDARD2_0
using Microsoft.AspNetCore.Hosting;
#endif
using Microsoft.Extensions.DependencyInjection;
#if NETCOREAPP3_1
using Microsoft.Extensions.Hosting;
#endif
using Microsoft.Extensions.Options;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Options;

namespace NetModular.Lib.Module.AspNetCore
{
    public static class ApplicationBuilderExtensions
    {
#if NETSTANDARD2_0
        public static IApplicationBuilder UseModules(this IApplicationBuilder app, IHostingEnvironment env)
#elif NETCOREAPP3_1
        public static IApplicationBuilder UseModules(this IApplicationBuilder app, IHostEnvironment env)
#endif
        {
            app.UseUpload();

            app.UseTemp();

            var modules = app.ApplicationServices.GetService<IModuleCollection>();
            foreach (var module in modules)
            {
                //加载模块初始化器
                ((ModuleDescriptor)module).Initializer?.Configure(app, env);
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

        /// <summary>
        /// 设置文件上传路径
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        private static void UseUpload(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<IOptionsMonitor<ModuleCommonOptions>>().CurrentValue;

            if (options.UploadPath.IsNull())
            {
                //默认在程序根目录下的Upload目录
                options.UploadPath = Path.Combine(AppContext.BaseDirectory, "Upload");
            }

            if (!Directory.Exists(options.UploadPath))
            {
                Directory.CreateDirectory(options.UploadPath);
            }
        }

        /// <summary>
        /// 设置临时文件路径
        /// </summary>
        /// <param name="app"></param>
        private static void UseTemp(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<IOptionsMonitor<ModuleCommonOptions>>().CurrentValue;

            if (options.TempPath.IsNull())
            {
                //默认在程序根目录下的Temp目录
                options.TempPath = Path.Combine(AppContext.BaseDirectory, "Temp");
            }
        }
    }
}
