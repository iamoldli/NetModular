using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nm.Lib.Cache.Abstractions;
using StackExchange.Redis;

namespace Nm.Lib.Cache.Redis
{
    public class RedisCacheHandler : ICacheHandler
    {
        private readonly IDatabase _db;

        public RedisCacheHandler(RedisHelper helper)
        {
            _db = helper.GetDb();
        }

        public string Get(string key)
        {
            return _db.StringGet(key);
        }

        public T Get<T>(string key)
        {
            var value = _db.StringGet(key);
            if (value.HasValue)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return default;
        }

        public async Task<string> GetAsync(string key)
        {
            return await _db.StringGetAsync(key);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);
            if (value.HasValue)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return default;
        }

        public bool TryGetValue(string key, out string value)
        {
            value = null;
            if (Exists(key))
            {
                value = Get(key);
                return true;
            }

            return false;
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            value = default;
            if (Exists(key))
            {
                value = Get<T>(key);
                return true;
            }

            return false;
        }

        public void Set<T>(string key, T value)
        {
            var valueJson = JsonConvert.SerializeObject(value);
            _db.StringSet(key, valueJson);
        }

        public void Set<T>(string key, T value, int expires)
        {
            var valueJson = JsonConvert.SerializeObject(value);
            _db.StringSet(key, valueJson, new TimeSpan(0, 0, expires, 0));
        }

        public Task SetAsync<T>(string key, T value)
        {
            var valueJson = JsonConvert.SerializeObject(value);
            return _db.StringSetAsync(key, valueJson);
        }

        public Task SetAsync<T>(string key, T value, int expires)
        {
            var valueJson = JsonConvert.SerializeObject(value);
            return _db.StringSetAsync(key, valueJson, new TimeSpan(0, 0, expires, 0));
        }

        public void Remove(string key)
        {
            _db.KeyDelete(key);
        }

        public Task RemoveAsync(string key)
        {
            return _db.KeyDeleteAsync(key);
        }

        public bool Exists(string key)
        {
            return _db.KeyExists(key);
        }

        public Task<bool> ExistsAsync(string key)
        {
            return _db.KeyExistsAsync(key);
        }
    }
}
