using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetModular.Lib.Utils.Core.Options
{
    public interface IModuleOptionsConfigure
    {
        /// <summary>
        /// 配置模块配置项
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        void ConfigOptions(IServiceCollection services, IConfiguration configuration);
    }
}
