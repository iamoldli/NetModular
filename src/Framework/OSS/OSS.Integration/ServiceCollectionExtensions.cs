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

            if (config.Qiniu != null && config.Qiniu.Domain.NotNull() && !config.Qiniu.Domain.EndsWith("/"))
            {
                config.Qiniu.Domain += "/";
            }
            if (config.Aliyun != null && config.Aliyun.Domain.NotNull() && !config.Aliyun.Domain.EndsWith("/"))
            {
                config.Aliyun.Domain += "/";
            }

            services.AddSingleton(config);

            var assembly = AssemblyHelper.LoadByNameEndString($".Lib.OSS.{config.Provider.ToString()}");
            if (assembly == null)
                return services;

            var providerType = assembly.GetTypes().FirstOrDefault(m => typeof(IFileStorageProvider).IsAssignableFrom(m));
            if (providerType != null)
            {
                services.AddSingleton(typeof(IFileStorageProvider), providerType);
            }

            return services;
        }
    }
}