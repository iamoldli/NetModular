using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Utils.Core.Helpers;

namespace NetModular.Lib.Config.Abstractions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加配置项核心服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddConfigCore(this IServiceCollection services)
        {
            if (services.All(m => m.ServiceType != typeof(IConfigCollection)))
            {
                services.AddImplementTypes();
            }

            return services;
        }


        /// <summary>
        /// 加载所有配置实现
        /// </summary>
        /// <param name="services"></param>
        private static void AddImplementTypes(this IServiceCollection services)
        {
            var configs = new ConfigCollection();
            var assemblies = AssemblyHelper.Load();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().Where(m => typeof(IConfig).IsImplementType(m) && typeof(IConfig) != m);
                foreach (var implType in types)
                {
                    if (implType.FullName.NotNull())
                    {
                        var descriptor = new ConfigDescriptor
                        {
                            Type = implType.FullName.Contains(".Lib.") ? ConfigType.Library : ConfigType.Module,
                            Code = implType.Name.Replace("Config", ""),
                            ImplementType = implType
                        };

                        configs.Add(descriptor);
                    }
                }
            }

            services.AddChangedEvent(assemblies, configs);

            services.AddSingleton<IConfigCollection>(configs);
        }

        /// <summary>
        /// 注入变更事件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <param name="configs"></param>
        private static void AddChangedEvent(this IServiceCollection services, List<Assembly> assemblies, ConfigCollection configs)
        {
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().Where(m => typeof(IConfigChangeEvent<>).IsImplementType(m));
                foreach (var implType in types)
                {
                    var configType = implType.GetInterfaces().First().GetGenericArguments()[0];
                    var configDescriptor = configs.FirstOrDefault(m => m.ImplementType == configType);
                    if (configDescriptor != null)
                    {
                        configDescriptor.ChangeEvents.Add(implType);

                        services.AddSingleton(implType);
                    }
                }
            }
        }
    }
}
