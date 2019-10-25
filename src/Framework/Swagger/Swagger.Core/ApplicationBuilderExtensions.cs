using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Module.Abstractions;

namespace NetModular.Lib.Swagger.Core
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 自定义Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            var modules = app.ApplicationServices.GetService<IModuleCollection>();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                if (modules == null) return;

                foreach (var moduleInfo in modules)
                {
                    c.SwaggerEndpoint($"/swagger/{moduleInfo.Id}/swagger.json", moduleInfo.Name);
                }
            });

            return app;
        }
    }
}
