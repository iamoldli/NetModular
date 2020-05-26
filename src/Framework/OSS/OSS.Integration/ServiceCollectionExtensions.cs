using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.OSS.Abstractions;
using NetModular.Lib.Utils.Core.Helpers;

namespace NetModular.Lib.OSS.Integration
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加OSS功能
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public static IServiceCollection AddOSS(this IServiceCollection services, IConfiguration cfg)
        {
            var config = new OSSConfig();
            var section = cfg.GetSection("OSS");
            if (section != null)
            {
                section.Bind(config);
            }

            services.AddSingleton(config);

            var assembly = AssemblyHelper.LoadByNameEndString($".Lib.OSS.{config.Provider.ToString()}");

            Check.NotNull(assembly, $"OSS实现程序集({config.Provider.ToString()})未找到，请安装 NetModular.Lib.OSS.{config.Provider.ToString()} 包");

            var providerType = assembly.GetTypes().FirstOrDefault(m => typeof(IFileStorageProvider).IsAssignableFrom(m));
            if (providerType != null)
            {
                services.AddSingleton(typeof(IFileStorageProvider), providerType);
            }

            return services;
        }
    }
}