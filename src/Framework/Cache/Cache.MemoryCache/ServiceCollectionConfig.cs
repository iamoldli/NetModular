using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Cache.Abstractions;

namespace NetModular.Lib.Cache.MemoryCache
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
