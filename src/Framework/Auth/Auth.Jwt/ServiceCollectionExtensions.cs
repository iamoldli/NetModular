using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetModular.Lib.Auth.Web;

namespace NetModular.Lib.Auth.Jwt
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Jwt认证
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddJwtAuth(this IServiceCollection services)
        {
            services.AddSingleton<MyJwtSecurityTokenHandler>();

            //从服务容器中获取自定义令牌验证处理器
            var securityTokenHandler = services.BuildServiceProvider().GetService<MyJwtSecurityTokenHandler>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                    };

                    //先清除再添加自定义令牌验证器
                    options.SecurityTokenValidators.Clear();
                    options.SecurityTokenValidators.Add(securityTokenHandler);
                });

            //注入权限集合
            services.AddScoped<PermissionCollection>();

            return services;
        }
    }
}
