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
                    options.TokenHandlers.Clear();
                    options.TokenHandlers.Add(new MyJwtSecurityTokenHandler());
                });

            //注入权限集合
            services.AddScoped<PermissionCollection>();

            return services;
        }
    }
}
