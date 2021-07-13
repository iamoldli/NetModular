using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Module.AspNetCore;
using NetModular.Lib.Swagger.Core.Extensions;

namespace NetModular.Lib.Swagger.Core
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 自定义Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <param name="pathBase"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, string pathBase = null)
        {
            var modules = app.ApplicationServices.GetService<IModuleCollection>();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                if (modules == null) return;

                foreach (var module in modules)
                {
                    if (((ModuleDescriptor)module).Initializer == null)
                        continue;

                    foreach (var g in module.GetGroups())
                    {
                        //var url = $"/swagger/{g.Key}/swagger.json";
                        //c.SwaggerEndpoint(pathBase.NotNull() ? $"{pathBase}{url}" : url, g.Value);
                        var url = $"{g.Key}/swagger.json";
                        c.SwaggerEndpoint(url, g.Value);
                    }
                }
            });

            return app;
        }
    }
}
