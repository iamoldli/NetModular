using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Utils.Core.Helpers;

namespace NetModular.Lib.Auth.Jwt
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Jwt认证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env"></param>
        public static IServiceCollection AddJwtAuth(this IServiceCollection services, IHostingEnvironment env)
        {
            var cfgHelper = new ConfigurationHelper();
            var jwtOptions = cfgHelper.Get<JwtOptions>("Jwt", env.EnvironmentName);

            services.AddSingleton(jwtOptions);
            services.TryAddSingleton(typeof(ILoginHandler), typeof(JwtLoginHandler));
            services.TryAddSingleton(typeof(LoginInfo));

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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                    };
                });

            return services;
        }
    }
}
