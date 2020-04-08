using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Web.TenantResolvers;

namespace NetModular.Lib.Auth.Web
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Web认证服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebAuth(this IServiceCollection services)
        {
            //登录信息服务
            services.TryAddSingleton<ILoginInfo, LoginInfo>();

            //默认租户解析服务
            services.TryAddSingleton<ITenantResolver, DefaultTenantResolver>();

            return services;
        }
    }
}
