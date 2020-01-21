using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Web;
using NetModular.Lib.Utils.Core.Helpers;

namespace NetModular.Lib.Auth.Jwt
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Jwt认证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="environmentName">环境名称</param>
        public static IServiceCollection AddJwtAuth(this IServiceCollection services, string environmentName)
        {
            var cfgHelper = new ConfigurationHelper();
            var jwtOptions = cfgHelper.Get<JwtOptions>("Jwt", environmentName);
            if (jwtOptions == null)
                return services;

            services.AddSingleton(jwtOptions);
            services.TryAddSingleton<ILoginHandler, JwtLoginHandler>();
            services.TryAddSingleton<ILoginInfo, LoginInfo>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}
