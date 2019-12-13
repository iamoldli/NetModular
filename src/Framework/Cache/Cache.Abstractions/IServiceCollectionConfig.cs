using Microsoft.Extensions.DependencyInjection;

namespace NetModular.Lib.Cache.Abstractions
{
    /// <summary>
    /// 缓存依赖注入服务配置
    /// </summary>
    public interface IServiceCollectionConfig
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options">配置项</param>
        /// <returns></returns>
        IServiceCollection Config(IServiceCollection services, CacheOptions options);
    }
}
