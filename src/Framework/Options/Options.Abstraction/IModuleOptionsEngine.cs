using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace NetModular.Lib.Options.Abstraction
{
    /// <summary>
    /// 配置容器接口
    /// </summary>
    public interface IModuleOptionsEngine
    {
        /// <summary>
        /// 从存储中加载配置信息
        /// </summary>
        /// <returns></returns>
        void LoadFromStorage(IServiceCollection services);

        /// <summary>
        /// 获取指定模块的配置项定义信息列表
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        List<ModuleOptionDefinitionAttribute> GetDefinitions(string moduleCode);

        /// <summary>
        /// 获取模块配置实例
        /// </summary>
        /// <param name="moduleCode">模块编码</param>
        /// <returns></returns>
        IModuleOptions GetInstance(string moduleCode);

        /// <summary>
        /// 刷新模块配置实例
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <param name="values"></param>
        void RefreshInstance(string moduleCode, Dictionary<string, object> values);
    }
}
