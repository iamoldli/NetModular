using Microsoft.Extensions.DependencyInjection;

namespace Nm.Lib.Cache.Abstractions
{
    /// <summary>
    /// 依赖注入服务配置
    /// </summary>
    public interface IServiceCollectionConfig
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        IServiceCollection Config(IServiceCollection services);
    }
}
