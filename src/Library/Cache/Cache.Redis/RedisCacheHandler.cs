using System;
using System.Threading.Tasks;
using Nm.Lib.Cache.Abstractions;

namespace Nm.Lib.Cache.Redis
{
    public class RedisCacheHandler : ICacheHandler
    {
        public string Get(string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out string value)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryGetValueAsync(string key, out string value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryGetValueAsync<T>(string key, out T value)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T value, int expires = 120)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync<T>(string key, T value, int expires = 120)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }
    }
}
