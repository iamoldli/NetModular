using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Utils.Core;
using NetModular.Lib.Utils.Core.Extensions;
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
            //通用配置
            var options = new ConfigurationHelper().Get<CacheOptions>("module", environmentName, true);
            if (options == null)
                return services;

            services.AddSingleton(options);

            var assembly = AssemblyHelper.LoadByNameEndString($".Lib.Cache.{options.Mode.ToString()}");

            Check.NotNull(assembly, $"缓存实现程序集{options.Mode.ToDescription()}未找到");

            var configType = assembly.GetTypes().FirstOrDefault(m => m.Name.Equals("ServiceCollectionConfig"));
            if (configType != null)
            {
                var instance = (IServiceCollectionConfig)Activator.CreateInstance(configType);
                instance.Config(services, options);
            }

            return services;
        }
    }
}
