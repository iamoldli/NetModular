using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Config.Abstraction;
using NetModular.Lib.Module.Abstractions;
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
        private readonly IModuleCollection _moduleCollection;
        private readonly ILogger _logger;

        public ConfigContainer(ICacheHandler cacheHandler, IConfigStorage storage, IModuleCollection moduleCollection, ILogger<ConfigContainer> logger)
        {
            _cacheHandler = cacheHandler;
            _storage = storage;
            _moduleCollection = moduleCollection;
            _logger = logger;
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
                            property.SetValue(config, Convert.ChangeType(configDescriptor.Value, property.PropertyType));
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

        /// <summary>
        /// 解析所有配置信息
        /// </summary>
        /// <returns></returns>
        public async Task RefreshAll()
        {
            if (_moduleCollection != null)
            {
                _logger.LogInformation("刷新所有配置项");

                //查询所有
                var list = await _storage.GetAll();

                foreach (var module in _moduleCollection)
                {
                    _logger.LogInformation("刷新配置项：{@name}", module.Name);

                    var configTypes = module.AssemblyDescriptor.Infrastructure.GetTypes().Where(m => typeof(IConfig).IsAssignableFrom(m)).ToList();
                    if (configTypes.Any())
                    {
                        foreach (var configType in configTypes)
                        {
                            var config = Activator.CreateInstance(configType);
                            var properties = configType.GetProperties();
                            foreach (var property in properties)
                            {
                                var key = configType.FullName + "." + property.Name;
                                if (!list.Any(m => m.Key.EqualsIgnoreCase(key)))
                                {
                                    var configDescriptor = new ConfigDescriptor
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

                                    var desc = property.GetCustomAttributes(false).FirstOrDefault(m => m.GetType() == typeof(DescriptionAttribute));
                                    if (desc != null)
                                    {
                                        configDescriptor.Remarks = ((DescriptionAttribute)desc).Description;
                                    }

                                    await _storage.Add(configDescriptor);

                                    _logger.LogInformation("新增配置：{@key}", key);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
