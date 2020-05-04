using System;
using System.Threading.Tasks;
using NetModular.Lib.Cache.Abstractions;

namespace NetModular.Lib.Cache.Redis
{
    public class RedisCacheHandler : ICacheHandler
    {
        private readonly RedisHelper _helper;

        public RedisCacheHandler(RedisHelper helper)
        {
            _helper = helper;
        }

        public string Get(string key)
        {
            return _helper.StringGetAsync<string>(key).GetAwaiter().GetResult();
        }

        public T Get<T>(string key)
        {
            return _helper.StringGetAsync<T>(key).GetAwaiter().GetResult();
        }

        public Task<string> GetAsync(string key)
        {
            return _helper.StringGetAsync<string>(key);
        }

        public Task<T> GetAsync<T>(string key)
        {
            return _helper.StringGetAsync<T>(key);
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

        public bool Set<T>(string key, T value)
        {
            return _helper.StringSetAsync(key, value).GetAwaiter().GetResult();
        }

        public bool Set<T>(string key, T value, int expires)
        {
            return _helper.StringSetAsync(key, value, new TimeSpan(0, 0, expires, 0)).GetAwaiter().GetResult();
        }

        public Task<bool> SetAsync<T>(string key, T value)
        {
            return _helper.StringSetAsync(key, value);
        }

        public Task<bool> SetAsync<T>(string key, T value, int expires)
        {
            return _helper.StringSetAsync(key, value, new TimeSpan(0, 0, expires, 0));
        }

        public bool Remove(string key)
        {
            return _helper.KeyDelete(key);
        }

        public Task<bool> RemoveAsync(string key)
        {
            return _helper.KeyDeleteAsync(key);
        }

        public bool Exists(string key)
        {
            return _helper.KeyExists(key);
        }

        public Task<bool> ExistsAsync(string key)
        {
            return _helper.KeyExistsAsync(key);
        }

        public Task RemoveByPrefixAsync(string prefix)
        {
            return _helper.DeleteByPrefix(prefix);
        }
    }
}
