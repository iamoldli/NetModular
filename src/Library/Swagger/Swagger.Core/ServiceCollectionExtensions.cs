using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Nm.Lib.Module.Abstractions;
using Nm.Lib.Swagger.Core.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace Nm.Lib.Swagger.Core
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

                //添加Jwt验证设置
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    { "Bearer", Enumerable.Empty<string>() },
                });

                //链接转小写过滤器
                c.DocumentFilter<LowercaseDocumentFilter>();

                //描述信息处理
                c.DocumentFilter<DescriptionDocumentFilter>();

                //隐藏属性
                c.SchemaFilter<IgnorePropertySchemaFilter>();
            });

            return services;
        }
    }
}
