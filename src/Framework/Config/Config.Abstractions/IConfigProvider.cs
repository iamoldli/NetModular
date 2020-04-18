using System;

namespace NetModular.Lib.Config.Abstractions
{
    /// <summary>
    /// 配置提供器接口
    /// </summary>
    public interface IConfigProvider
    {
        /// <summary>
        /// 获取指定实现类的实例
        /// </summary>
        /// <param name="implementType"></param>
        /// <returns></returns>
        IConfig Get(Type implementType);

        /// <summary>
        /// 获取配置实例
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        IConfig Get(string code, ConfigType type);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <returns></returns>
        TConfig Get<TConfig>() where TConfig : IConfig, new();

        /// <summary>
        /// 设置配置类
        /// </summary>
        /// <param name="type"></param>
        /// <param name="code"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        bool Set(ConfigType type, string code, string json);
    }
}
