using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Nm.Lib.Module.Abstractions;
using Nm.Lib.Swagger.Core.Filters;

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
                        c.SwaggerDoc(moduleInfo.Id, new OpenApiInfo
                        {
                            Title = moduleInfo.Name,
                            Version = moduleInfo.Version
                        });
                    }
                }

                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT认证请求头格式: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme= "Bearer"
                };

                //添加设置Token的按钮
                c.AddSecurityDefinition("Bearer", securityScheme);

                //添加Jwt验证设置
                var securityRequirement = new OpenApiSecurityRequirement { { securityScheme, new List<string>() } };
                c.AddSecurityRequirement(securityRequirement);

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
