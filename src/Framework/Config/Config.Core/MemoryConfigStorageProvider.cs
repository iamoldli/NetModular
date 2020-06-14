using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Config.Abstractions;

namespace NetModular.Lib.Config.Core
{
    /// <summary>
    /// 内存配置存储
    /// </summary>
    public class MemoryConfigStorageProvider : IConfigStorageProvider
    {
        private static readonly Dictionary<string, string> ConfigJsons = new Dictionary<string, string>();

        public Task<string> GetJson(ConfigType type, string code)
        {
            var json = ConfigJsons.FirstOrDefault(m => m.Key == $"{type.ToInt()}_{code.ToLower()}").Value;
            return Task.FromResult(json);
        }

        public Task<bool> SaveJson(ConfigType type, string code, string json)
        {
            var key = $"{type.ToInt()}_{code.ToLower()}";
            var keyValue = ConfigJsons.FirstOrDefault(m => m.Key == key);
            if (keyValue.Key != null)
            {
                ConfigJsons.Remove(keyValue.Key);
            }

            ConfigJsons.Add(key, json);
            return Task.FromResult(true);
        }
    }
}
