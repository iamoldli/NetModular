using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NetModular.Lib.Cache.Redis;
using NetModular.Lib.Config.Abstractions;
using Newtonsoft.Json;

namespace NetModular.Lib.Config.Redis
{
    public class ConfigProvider : IConfigProvider
    {
        private const string CACHE_KEY = "CONFIG";
        private readonly RedisHelper _redisHelper;
        private readonly IConfigCollection _configs;
        private readonly IRedisSerializer _redisSerializer;
        private readonly IConfigStorageProvider _storageProvider;
        private readonly IConfiguration _cfg;

        public ConfigProvider(RedisHelper redisHelper, IConfigCollection configs, IRedisSerializer redisSerializer, IConfigStorageProvider storageProvider, IConfiguration cfg)
        {
            _redisHelper = redisHelper;
            _configs = configs;
            _redisSerializer = redisSerializer;
            _storageProvider = storageProvider;
            _cfg = cfg;
        }

        public IConfig Get(Type implementType)
        {
            var descriptor = _configs.FirstOrDefault(m => m.ImplementType == implementType);
            if (descriptor == null)
                throw new NotImplementedException("没有找到配置类型");

            return Get(descriptor);
        }

        public IConfig Get(string code, ConfigType type)
        {
            var descriptor = _configs.FirstOrDefault(m => m.Code.EqualsIgnoreCase(code) && m.Type == type);
            if (descriptor == null)
                throw new NotImplementedException("没有找到配置类型");

            return Get(descriptor);
        }

        public TConfig Get<TConfig>() where TConfig : IConfig, new()
        {
            var descriptor = _configs.FirstOrDefault(m => m.ImplementType == typeof(TConfig));
            if (descriptor == null)
                throw new NotImplementedException("没有找到配置类型");

            return (TConfig)Get(descriptor);
        }

        public bool Set(ConfigType type, string code, string json)
        {
            var descriptor = _configs.FirstOrDefault(m => m.Code.EqualsIgnoreCase(code) && m.Type == type);
            if (descriptor == null)
                throw new NotImplementedException("没有找到配置类型");

            var key = _redisHelper.GetKey($"{CACHE_KEY}:{descriptor.Type.ToString().ToUpper()}:{descriptor.Code.ToUpper()}");
            var config = JsonConvert.DeserializeObject(json, descriptor.ImplementType);

            _redisHelper.Db.StringSetAsync(key, _redisSerializer.Serialize(config)).GetAwaiter().GetResult();

            return true;
        }

        private IConfig Get(ConfigDescriptor descriptor)
        {
            var key = _redisHelper.GetKey($"{CACHE_KEY}:{descriptor.Type.ToString().ToUpper()}:{descriptor.Code.ToUpper()}");
            var cache = _redisHelper.Db.StringGetAsync(key).Result;
            if (cache.HasValue)
            {
                return (IConfig)_redisSerializer.Deserialize(cache, descriptor.ImplementType);
            }

            IConfig config;
            var json = _storageProvider.GetJson(descriptor.Type, descriptor.Code).Result;
            if (json.IsNull())
            {
                config = (IConfig)Activator.CreateInstance(descriptor.ImplementType);
                if (descriptor.Type == ConfigType.Library)
                {
                    var section = _cfg.GetSection(descriptor.Code);
                    if (section != null)
                    {
                        section.Bind(config);
                    }
                }
            }
            else
                config = (IConfig)JsonConvert.DeserializeObject(json, descriptor.ImplementType);

            _redisHelper.Db.StringSetAsync(key, _redisSerializer.Serialize(config)).GetAwaiter().GetResult();

            return config;
        }
    }
}
