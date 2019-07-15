using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Nm.Lib.Cache.Abstractions;
using Nm.Lib.Utils.Core;
using Nm.Lib.Utils.Core.Helpers;

namespace Nm.Lib.Cache.Integration
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="services"></param>
        /// <param name="environmentName"></param>
        /// <returns></returns>
        public static IServiceCollection AddCache(this IServiceCollection services, string environmentName)
        {
            var cfgHelper = new ConfigurationHelper();
            var cacheOptions = cfgHelper.Get<CacheOptions>("Cache", environmentName);

            if (cacheOptions == null)
                return services;

            services.AddSingleton(cacheOptions);

            var ns = typeof(ServiceCollectionExtensions).Namespace;

            Check.NotNull(ns, "Namespace is null");

            var name = ns.Replace("Integration", cacheOptions.Mode.ToString());

            var assemblyHelper = new AssemblyHelper();
            var assembly = assemblyHelper.Load(m => m.Name.Equals(name)).FirstOrDefault();

            Check.NotNull(assembly, cacheOptions.Mode + "缓存实现程序集未找到");

            var configType = assembly.GetTypes().FirstOrDefault(m => m.Name.Equals("ServiceCollectionConfig"));
            if (configType != null)
            {
                var instance = (IServiceCollectionConfig)Activator.CreateInstance(configType);
                instance.Config(services, cacheOptions);
            }

            return services;
        }
    }
}
