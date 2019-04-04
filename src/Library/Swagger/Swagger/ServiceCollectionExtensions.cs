using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Swagger.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace NetModular.Lib.Swagger
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <param name="modules">模块集合</param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services, IModuleCollection modules)
        {
            services.AddSwaggerGen(c =>
            {
                if (modules != null)
                {
                    foreach (var moduleInfo in modules)
                    {
                        c.SwaggerDoc(moduleInfo.Id, new Info
                        {
                            Title = moduleInfo.Name,
                            Version = moduleInfo.Version
                        });

                        c.IncludeXmlComments(moduleInfo.DocPath);
                    }
                }

                //添加设置Token的按钮
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT认证请求头格式: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    { "Bearer", Enumerable.Empty<string>() },
                });

                //链接转小写过滤器
                c.DocumentFilter<LowercaseDocumentFilter>();

                c.DocumentFilter<DescriptionDocumentFilter>();

                //隐藏属性
                c.SchemaFilter<IgnorePropertySchemaFilter>();
            });

            return services;
        }
    }
}
