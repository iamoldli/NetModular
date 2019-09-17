using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;
using Nm.Lib.Cache.Abstractions;
using Nm.Lib.Utils.Core;
using Nm.Lib.Utils.Core.Extensions;

namespace Nm.Lib.Cache.Redis
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

        #region ==String==

        public Task<bool> StringSetAsync<T>(string key, T obj)
        {
            if (IsBaseType<T>())
            {
                return Db.StringSetAsync(key, JsonConvert.SerializeObject(obj));
            }

            return Db.StringSetAsync(key, obj.ToString());
        }

        public async Task<T> StringGetAsync<T>(string key, T obj)
        {
            var cache = await Db.StringGetAsync(key);
            if (cache.HasValue)
            {
                if (IsBaseType<T>())
                {
                    return JsonConvert.DeserializeObject<T>(cache);
                }

                return cache.To<T>();
            }

            return default;
        }

        /// <summary>
        /// 字符串减去数值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> StringDecrementAsync(string key, long value = 1)
        {
            return Db.StringDecrementAsync(key, value);
        }

        /// <summary>
        /// 字符串增加数值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> StringIncrementAsync(string key, long value = 1)
        {
            return Db.StringIncrementAsync(key, value);
        }

        #endregion

        #region ==Hash==

        /// <summary>
        /// 设置值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Task HashSetAsync<T>(string key, string field, T obj)
        {
            if (IsBaseType<T>())
            {
                return Db.HashSetAsync(key, field, JsonConvert.SerializeObject(obj));
            }

            return Db.HashSetAsync(key, field, obj.ToString());
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public async Task<T> HashGetAsync<T>(string key, string field)
        {
            var cache = await Db.HashGetAsync(key, field);
            if (cache.HasValue)
            {
                if (IsBaseType<T>())
                {
                    return JsonConvert.DeserializeObject<T>(cache);
                }

                return cache.To<T>();
            }

            return default;
        }

        /// <summary>
        /// 获取所有值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IList<T>> HashValuesAsync<T>(string key)
        {
            var cache = await Db.HashValuesAsync(key);
            if (cache.Any())
            {
                return cache.Select(m => IsBaseType<T>() ? JsonConvert.DeserializeObject<T>(m) : m.To<T>()).ToList();
            }

            return null;
        }

        /// <summary>
        /// 删除值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public Task HashDeleteAsync(string key, string field)
        {
            return Db.HashDeleteAsync(key, field);
        }

        /// <summary>
        /// 获取所有键值集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IList<KeyValuePair<string, T>>> HashGetAllAsync<T>(string key)
        {
            var cache = await Db.HashGetAllAsync(key);
            if (cache.Any())
            {
                return cache.Select(m => new KeyValuePair<string, T>(m.Name.ToString(), IsBaseType<T>() ? JsonConvert.DeserializeObject<T>(m.Value) : m.Value.To<T>())).ToList();
            }

            return null;
        }

        /// <summary>
        /// 数值减
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public Task<long> HashDecrementAsync(string key, string field, long value = 1)
        {
            return Db.HashDecrementAsync(key, field, value);
        }

        /// <summary>
        /// 数值加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public Task<long> HashIncrementAsync(string key, string field, long value = 1)
        {
            return Db.HashIncrementAsync(key, field, value);
        }

        #endregion

        #region ==Set==

        public Task<bool> SetAddAsync<T>(string key, T obj)
        {
            if (IsBaseType<T>())
            {
                return Db.SetAddAsync(key, JsonConvert.SerializeObject(obj));
            }

            return Db.SetAddAsync(key, obj.ToString());
        }

        public Task<bool> SetRemoveAsync<T>(string key, T obj)
        {
            if (IsBaseType<T>())
            {
                return Db.SetRemoveAsync(key, JsonConvert.SerializeObject(obj));
            }
            return Db.SetAddAsync(key, obj.ToString());
        }

        public Task<bool> SetContainsAsync<T>(string key, T obj)
        {
            if (IsBaseType<T>())
            {
                return Db.SetContainsAsync(key, JsonConvert.SerializeObject(obj));
            }
            return Db.SetContainsAsync(key, obj.ToString());
        }

        public Task<long> SetLengthAsync<T>(string key)
        {
            return Db.SetLengthAsync(key);
        }

        public async Task<T> SetPopAsync<T>(string key)
        {
            var cache = await Db.SetPopAsync(key);
            if (cache.HasValue)
            {
                if (IsBaseType<T>())
                {
                    return JsonConvert.DeserializeObject<T>(cache);
                }

                return cache.To<T>();
            }

            return default;
        }

        #endregion

        public void Dispose()
        {
            _redis?.Dispose();
        }

        /// <summary>
        /// 是否是基础类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private bool IsBaseType<T>()
        {
            return Type.GetTypeCode(typeof(T)) == TypeCode.Object;
        }
    }
}
