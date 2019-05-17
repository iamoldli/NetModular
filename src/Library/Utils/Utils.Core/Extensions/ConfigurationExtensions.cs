using Microsoft.Extensions.Configuration;

namespace Nm.Lib.Utils.Core.Extensions
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
    }
}
