using Microsoft.Extensions.DependencyInjection;
using Tm.Lib.Cache.Abstractions;

namespace Tm.Lib.Cache.MemoryCache
{
    public class ServiceCollectionConfig : IServiceCollectionConfig
    {
        public IServiceCollection Config(IServiceCollection services, CacheOptions options)
        {
            services.AddMemoryCache();

            services.AddSingleton<ICacheHandler, MemoryCacheHandler>();

            return services;
        }
    }
}
