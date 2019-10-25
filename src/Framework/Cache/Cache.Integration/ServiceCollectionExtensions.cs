using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Utils.Core;
using NetModular.Lib.Utils.Core.Helpers;

namespace NetModular.Lib.Cache.Integration
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

            var assembly = AssemblyHelper.LoadByNameEndString($".Lib.Cache.{cacheOptions.Mode.ToString()}");

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
