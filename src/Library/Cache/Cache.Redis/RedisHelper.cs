using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tm.Lib.Cache.Abstractions;
using Tm.Lib.Utils.Core;
using StackExchange.Redis;

namespace Tm.Lib.Cache.Redis
{
    public class RedisHelper : IDisposable
    {
        private readonly ConnectionMultiplexer _redis;
        public IDatabase Db;

        public RedisHelper(RedisOptions options)
        {
            Check.NotNull(options.ConnectionString, nameof(RedisOptions), "未设置Redis连接信息");

            _redis = ConnectionMultiplexer.Connect(options.ConnectionString);
            Db = _redis.GetDatabase();
        }

        public IDatabase GetDb(int db = -1)
        {
            return _redis.GetDatabase(db);
        }

        public Task HashSetAsync<T>(string key, string field, T obj)
        {
            return Db.HashSetAsync(key, field, JsonConvert.SerializeObject(obj));
        }

        public async Task<T> HashGetAsync<T>(string key, string field)
        {
            var cache = await Db.HashGetAsync(key, field);
            if (cache.HasValue)
            {
                return JsonConvert.DeserializeObject<T>(cache);
            }

            return default;
        }

        public async Task<IList<T>> HashValuesAsync<T>(string key)
        {
            var cache = await Db.HashValuesAsync(key);
            if (cache.Any())
            {
                return cache.Select(m => JsonConvert.DeserializeObject<T>(m)).ToList();
            }

            return null;
        }

        public Task HashDeleteAsync(string key, string field)
        {
            return Db.HashDeleteAsync(key, field);
        }

        public void Dispose()
        {
            _redis?.Dispose();
        }
    }
}
