using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Nm.Lib.Cache.Abstractions;

namespace Nm.Lib.Cache.MemoryCache
{
    public class MemoryCacheHandler : ICacheHandler
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheHandler(IMemoryCache cache)
        {
            _cache = cache;
        }

        public string Get(string key)
        {
            var value = _cache.Get(key);
            return value?.ToString();
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public Task<string> GetAsync(string key)
        {
            var value = Get(key);
            return Task.FromResult(value);
        }

        public Task<T> GetAsync<T>(string key)
        {
            return Task.FromResult(_cache.Get<T>(key));
        }

        public bool TryGetValue(string key, out string value)
        {
            return _cache.TryGetValue(key, out value);
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            return _cache.TryGetValue(key, out value);
        }

        public Task<bool> TryGetValueAsync(string key, out string value)
        {
            return Task.FromResult(_cache.TryGetValue(key, out value));
        }

        public Task<bool> TryGetValueAsync<T>(string key, out T value)
        {
            return Task.FromResult(_cache.TryGetValue(key, out value));
        }

        public void Set<T>(string key, T value, int expires = 120)
        {
            _cache.Set(key, value, new TimeSpan(0, 0, expires, 0));
        }

        public Task SetAsync<T>(string key, T value, int expires = 120)
        {
            return Task.FromResult(_cache.Set(key, value, new TimeSpan(0, 0, expires, 0)));
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public Task RemoveAsync(string key)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }
    }
}
