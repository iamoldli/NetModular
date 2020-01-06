using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Config.Abstraction;
using NetModular.Lib.Utils.Core.Extensions;

namespace NetModular.Lib.Config.Core
{
    public class ConfigContainer : IConfigContainer
    {
        //配置项中数据库中的key与实体缓存key的关系表
        public readonly ConcurrentDictionary<string, string> CacheDic = new ConcurrentDictionary<string, string>();
        private const string CacheKeyPrefix = "CONFIG_";
        private readonly ICacheHandler _cacheHandler;
        private readonly IConfigStorage _storage;

        public ConfigContainer(ICacheHandler cacheHandler, IConfigStorage storage)
        {
            _cacheHandler = cacheHandler;
            _storage = storage;
        }

        public async Task<T> Resolve<T>() where T : IConfig, new()
        {
            var type = typeof(T);
            var cacheKey = CacheKeyPrefix + type.FullName;
            if (!_cacheHandler.TryGetValue(cacheKey, out T config))
            {
                config = new T();

                var list = await _storage.GetAll();
                if (list != null && list.Any())
                {
                    var properties = type.GetProperties();
                    foreach (var property in properties)
                    {
                        var key = type.FullName + "." + property.Name;
                        var configDescriptor = list.FirstOrDefault(m => m.Key.EqualsIgnoreCase(key));
                        if (configDescriptor != null)
                        {
                            property.SetValue(config, configDescriptor.Value);
                        }
                        else
                        {
                            configDescriptor = new ConfigDescriptor
                            {
                                Key = key,
                                Value = string.Empty,
                                Remarks = property.Name
                            };
                            var value = property.GetValue(config);
                            if (value != null)
                            {
                                configDescriptor.Value = value.ToString();
                            }

                            var desc = property.GetCustomAttributes(false)
                                .FirstOrDefault(m => m.GetType() == typeof(DescriptionAttribute));
                            if (desc != null)
                            {
                                configDescriptor.Remarks = ((DescriptionAttribute)desc).Description;
                            }

                            await _storage.Add(configDescriptor);
                        }
                        CacheDic.TryAdd(key, cacheKey);
                    }
                }

                await _cacheHandler.SetAsync(cacheKey, config);
            }

            return config;
        }

        public async Task Remove(string key)
        {
            var cacheKey = CacheDic.FirstOrDefault(m => m.Key.EqualsIgnoreCase(key));
            if (cacheKey.Key.NotNull())
            {
                await _cacheHandler.RemoveAsync(cacheKey.Value);
            }
        }
    }
}
