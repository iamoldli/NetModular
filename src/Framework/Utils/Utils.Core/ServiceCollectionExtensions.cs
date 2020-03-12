using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Lib.Utils.Core.Helpers;

namespace NetModular.Lib.Utils.Core
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 从指定程序集中注入服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddServicesFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            if (assembly == null)
                return services;

            foreach (var type in assembly.GetTypes())
            {
                #region ==单例注入==

                if (type.GetCustomAttributes().Any(m => m.GetType() == typeof(SingletonAttribute)))
                {
                    var interfaces = type.GetInterfaces();
                    if (interfaces.Any())
                    {
                        foreach (var i in interfaces)
                        {
                            services.AddSingleton(i, type);
                        }
                    }
                    else
                    {
                        services.AddSingleton(type);
                    }
                    continue;
                }

                #endregion

                #region ==瞬时注入==

                if (type.GetCustomAttributes().Any(m => m.GetType() == typeof(TransientAttribute)))
                {
                    var interfaces = type.GetInterfaces();
                    if (interfaces.Any())
                    {
                        foreach (var i in interfaces)
                        {
                            services.AddTransient(i, type);
                        }
                    }
                    else
                    {
                        services.AddTransient(type);
                    }
                    continue;
                }

                #endregion

                #region ==Scoped注入==

                if (type.GetCustomAttributes().Any(m => m.GetType() == typeof(ScopedAttribute)))
                {
                    var interfaces = type.GetInterfaces();
                    if (interfaces.Any())
                    {
                        foreach (var i in interfaces)
                        {
                            services.AddScoped(i, type);
                        }
                    }
                    else
                    {
                        services.AddScoped(type);
                    }
                }

                #endregion
            }

            return services;
        }

        /// <summary>
        /// 注入所有服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddNetModularServices(this IServiceCollection services)
        {
            var assemblies = AssemblyHelper.Load();
            foreach (var assembly in assemblies)
            {
                services.AddServicesFromAssembly(assembly);
            }
            return services;
        }
    }
}
