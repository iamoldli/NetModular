using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Utils.Core.Attributes;

namespace NetModular.Lib.Utils.Core
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 从指定程序集中注入单例服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddSingletonFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            if (assembly == null)
                return services;

            var serviceTypes = assembly.GetTypes()
                .Where(t => t.GetCustomAttributes().Any(m => m.GetType() == typeof(SingletonAttribute)));

            foreach (var serviceType in serviceTypes)
            {
                services.AddSingleton(serviceType);
            }

            return services;
        }

        /// <summary>
        /// 添加基础工具
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddUtils(this IServiceCollection services)
        {
            return services.AddSingletonFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
