using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace NetModular.Lib.Utils.Core.Extensions
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cfg"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this IConfiguration cfg, string key) where T : class, new()
        {
            if (cfg == null || key.IsNull())
                return new T();

            var t = cfg.GetSection(key).Get<T>();
            if (t == null)
                return new T();

            return t;
        }

        /// <summary>
        /// 根据配置名称加载指定的配置项
        /// </summary>
        /// <param name="configFileName">配置文件名称，使用约定，配置文件放在项目的config目录中，如logging配置：config/logging.json</param>
        /// <param name="environmentName"></param>
        /// <returns></returns>
        public static IConfiguration Load(string configFileName, string environmentName = "")
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(AppContext.BaseDirectory, "config"))
                 .AddJsonFile(configFileName + ".json", true);

            if (environmentName.NotNull())
            {
                builder.AddJsonFile(configFileName + "." + environmentName + ".json", true);
            }

            return builder.Build();
        }

        /// <summary>
        /// 根据配置名称加载指定的配置项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configFileName">配置文件名称，使用约定，配置文件放在项目的config目录中，如logging配置：config/logging.json</param>
        /// <param name="environmentName"></param>
        /// <returns></returns>
        public static T Get<T>(string configFileName, string environmentName = "")
        {
            var configuration = Load(configFileName, environmentName);
            return configuration.Get<T>();
        }
    }
}
