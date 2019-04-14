using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Lib.Utils.Core.Extensions;

namespace NetModular.Lib.Utils.Core.Helpers
{
    /// <summary>
    /// 配置帮助类
    /// </summary>
    [Singleton]
    public class ConfigurationHelper
    {
        /// <summary>
        /// 根据配置名称加载指定的配置项
        /// </summary>
        /// <param name="configFileName">配置文件名称，使用约定，配置文件放在项目的config目录中，如logging配置：config/logging.json</param>
        /// <param name="environmentName"></param>
        /// <param name="reloadOnChange">自动更新</param>
        /// <returns></returns>
        public IConfiguration Load(string configFileName, string environmentName = "", bool reloadOnChange = false)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory, "config"))
                .AddJsonFile(configFileName + ".json", true, reloadOnChange);

            if (environmentName.NotNull())
            {
                builder.AddJsonFile(configFileName + "." + environmentName + ".json", true, reloadOnChange);
            }

            return builder.Build();
        }

        /// <summary>
        /// 根据配置名称加载指定的配置项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configFileName">配置文件名称，使用约定，配置文件放在项目的config目录中，如logging配置：config/logging.json</param>
        /// <param name="environmentName"></param>
        /// <param name="reloadOnChange">自动更新</param>
        /// <returns></returns>
        public T Get<T>(string configFileName, string environmentName = "", bool reloadOnChange = false)
        {
            var configuration = Load(configFileName, environmentName, reloadOnChange);
            return configuration.Get<T>();
        }
    }
}
