using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using StackExchange.Redis;
using NetModular.Lib.Cache.Abstractions;

namespace NetModular.Lib.Cache.Redis
{
    public class RedisHelper : IDisposable
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly string _prefix;
        public IDatabase Db;

        public RedisHelper(RedisOptions options)
        {
            Check.NotNull(options.ConnectionString, nameof(RedisOptions), "未设置Redis连接信息");
            _prefix = options.Prefix ?? string.Empty;

            _redis = ConnectionMultiplexer.Connect(options.ConnectionString);
            Db = _redis.GetDatabase();
        }

        public IDatabase GetDb(int db = -1)
        {
            return _redis.GetDatabase(db);
        }

        /// <summary>
        /// 获取键(附加前缀)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetKey(string key)
        {
            return _prefix + key;
        }

        #region ==String==

        public bool StringSet<T>(string key, T obj, TimeSpan? expiry = null)
        {
            if (IsNotBaseType<T>())
            {
                return Db.StringSet(GetKey(key), JsonSerializer.Serialize(obj), expiry);
            }

            return Db.StringSet(GetKey(key), obj.ToString(), expiry);
        }

        public Task<bool> StringSetAsync<T>(string key, T obj, TimeSpan? expiry = null)
        {
            if (IsNotBaseType<T>())
            {
                return Db.StringSetAsync(GetKey(key), JsonSerializer.Serialize(obj), expiry);
            }

            return Db.StringSetAsync(GetKey(key), obj.ToString(), expiry);
        }

        public T StringGet<T>(string key)
        {
            var cache = Db.StringGet(GetKey(key));
            if (cache.HasValue)
            {
                if (IsNotBaseType<T>())
                {
                    return JsonSerializer.Deserialize<T>(cache);
                }

                return cache.To<T>();
            }

            return default;
        }

        public async Task<T> StringGetAsync<T>(string key)
        {
            var cache = await Db.StringGetAsync(GetKey(key));
            if (cache.HasValue)
            {
                if (IsNotBaseType<T>())
                {
                    return JsonSerializer.Deserialize<T>(cache);
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
        public long StringDecrement(string key, long value = 1)
        {
            return Db.StringDecrement(GetKey(key), value);
        }

        /// <summary>
        /// 字符串减去数值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> StringDecrementAsync(string key, long value = 1)
        {
            return Db.StringDecrementAsync(GetKey(key), value);
        }

        /// <summary>
        /// 字符串增加数值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long StringIncrement(string key, long value = 1)
        {
            return Db.StringIncrement(GetKey(key), value);
        }

        /// <summary>
        /// 字符串增加数值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> StringIncrementAsync(string key, long value = 1)
        {
            return Db.StringIncrementAsync(GetKey(key), value);
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
        public bool HashSet<T>(string key, string field, T obj)
        {
            if (IsNotBaseType<T>())
            {
                return Db.HashSet(GetKey(key), field, JsonSerializer.Serialize(obj));
            }

            return Db.HashSet(GetKey(key), field, obj.ToString());
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Task<bool> HashSetAsync<T>(string key, string field, T obj)
        {
            if (IsNotBaseType<T>())
            {
                return Db.HashSetAsync(GetKey(key), field, JsonSerializer.Serialize(obj));
            }

            return Db.HashSetAsync(GetKey(key), field, obj.ToString());
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public T HashGet<T>(string key, string field)
        {
            var cache = Db.HashGet(GetKey(key), field);
            if (cache.HasValue)
            {
                if (IsNotBaseType<T>())
                {
                    return JsonSerializer.Deserialize<T>(cache);
                }

                return cache.To<T>();
            }

            return default;
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
            var cache = await Db.HashGetAsync(GetKey(key), field);
            if (cache.HasValue)
            {
                if (IsNotBaseType<T>())
                {
                    return JsonSerializer.Deserialize<T>(cache);
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
        public IList<T> HashValues<T>(string key)
        {
            var cache = Db.HashValues(GetKey(key));
            if (cache.Any())
            {
                return cache.Select(m => IsNotBaseType<T>() ? JsonSerializer.Deserialize<T>(m) : m.To<T>()).ToList();
            }

            return null;
        }

        /// <summary>
        /// 获取所有值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IList<T>> HashValuesAsync<T>(string key)
        {
            var cache = await Db.HashValuesAsync(GetKey(key));
            if (cache.Any())
            {
                return cache.Select(m => IsNotBaseType<T>() ? JsonSerializer.Deserialize<T>(m) : m.To<T>()).ToList();
            }

            return null;
        }

        /// <summary>
        /// 删除值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool HashDelete(string key, string field)
        {
            return Db.HashDelete(GetKey(key), field);
        }

        /// <summary>
        /// 删除值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public Task<bool> HashDeleteAsync(string key, string field)
        {
            return Db.HashDeleteAsync(GetKey(key), field);
        }

        /// <summary>
        /// 获取所有键值集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public IList<KeyValuePair<string, T>> HashGetAll<T>(string key)
        {
            var cache = Db.HashGetAll(GetKey(key));
            if (cache.Any())
            {
                return cache.Select(m => new KeyValuePair<string, T>(m.Name.ToString(), IsNotBaseType<T>() ? JsonSerializer.Deserialize<T>(m.Value) : m.Value.To<T>())).ToList();
            }

            return null;
        }

        /// <summary>
        /// 获取所有键值集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IList<KeyValuePair<string, T>>> HashGetAllAsync<T>(string key)
        {
            var cache = await Db.HashGetAllAsync(GetKey(key));
            if (cache.Any())
            {
                return cache.Select(m => new KeyValuePair<string, T>(m.Name.ToString(), IsNotBaseType<T>() ? JsonSerializer.Deserialize<T>(m.Value) : m.Value.To<T>())).ToList();
            }

            return null;
        }

        /// <summary>
        /// 数值减
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long HashDecrement(string key, string field, long value = 1)
        {
            return Db.HashDecrement(GetKey(key), field, value);
        }

        /// <summary>
        /// 数值减
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> HashDecrementAsync(string key, string field, long value = 1)
        {
            return Db.HashDecrementAsync(GetKey(key), field, value);
        }

        /// <summary>
        /// 数值加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long HashIncrement(string key, string field, long value = 1)
        {
            return Db.HashIncrement(GetKey(key), field, value);
        }

        /// <summary>
        /// 数值加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> HashIncrementAsync(string key, string field, long value = 1)
        {
            return Db.HashIncrementAsync(GetKey(key), field, value);
        }

        #endregion

        #region ==Set==

        public bool SetAdd<T>(string key, T obj)
        {
            if (IsNotBaseType<T>())
            {
                return Db.SetAdd(GetKey(key), JsonSerializer.Serialize(obj));
            }

            return Db.SetAdd(GetKey(key), obj.ToString());
        }

        public Task<bool> SetAddAsync<T>(string key, T obj)
        {
            if (IsNotBaseType<T>())
            {
                return Db.SetAddAsync(GetKey(key), JsonSerializer.Serialize(obj));
            }

            return Db.SetAddAsync(GetKey(key), obj.ToString());
        }

        public bool SetRemove<T>(string key, T obj)
        {
            if (IsNotBaseType<T>())
            {
                return Db.SetRemove(GetKey(key), JsonSerializer.Serialize(obj));
            }
            return Db.SetRemove(GetKey(key), obj.ToString());
        }

        public Task<bool> SetRemoveAsync<T>(string key, T obj)
        {
            if (IsNotBaseType<T>())
            {
                return Db.SetRemoveAsync(GetKey(key), JsonSerializer.Serialize(obj));
            }
            return Db.SetRemoveAsync(GetKey(key), obj.ToString());
        }

        public bool SetContains<T>(string key, T obj)
        {
            if (IsNotBaseType<T>())
            {
                return Db.SetContains(GetKey(key), JsonSerializer.Serialize(obj));
            }
            return Db.SetContains(GetKey(key), obj.ToString());
        }

        public Task<bool> SetContainsAsync<T>(string key, T obj)
        {
            if (IsNotBaseType<T>())
            {
                return Db.SetContainsAsync(GetKey(key), JsonSerializer.Serialize(obj));
            }
            return Db.SetContainsAsync(GetKey(key), obj.ToString());
        }

        public long SetLength(string key)
        {
            return Db.SetLength(GetKey(key));
        }

        public Task<long> SetLengthAsync(string key)
        {
            return Db.SetLengthAsync(GetKey(key));
        }

        public T SetPop<T>(string key)
        {
            var cache = Db.SetPop(GetKey(key));
            if (cache.HasValue)
            {
                if (IsNotBaseType<T>())
                {
                    return JsonSerializer.Deserialize<T>(cache);
                }

                return cache.To<T>();
            }

            return default;
        }

        public async Task<T> SetPopAsync<T>(string key)
        {
            var cache = await Db.SetPopAsync(GetKey(key));
            if (cache.HasValue)
            {
                if (IsNotBaseType<T>())
                {
                    return JsonSerializer.Deserialize<T>(cache);
                }

                return cache.To<T>();
            }

            return default;
        }

        #endregion

        #region ==Sorted Set==

        /// <summary>
        /// 添加有序集合
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public Task<bool> SortedSetAddAsync<T>(string key, T member, double score)
        {
            if (IsNotBaseType<T>())
            {
                return Db.SortedSetAddAsync(GetKey(key), JsonSerializer.Serialize(member), score);
            }

            return Db.SortedSetAddAsync(GetKey(key), member.ToString(), score);
        }

        /// <summary>
        /// 减去值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<double> SortedSetDecrementAsync<T>(string key, T member, double value)
        {
            if (IsNotBaseType<T>())
            {
                return Db.SortedSetDecrementAsync(GetKey(key), JsonSerializer.Serialize(member), value);
            }
            return Db.SortedSetDecrementAsync(GetKey(key), member.ToString(), value);
        }

        /// <summary>
        /// 增加值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<double> SortedSetIncrementAsync<T>(string key, T member, double value)
        {
            if (IsNotBaseType<T>())
            {
                return Db.SortedSetIncrementAsync(GetKey(key), JsonSerializer.Serialize(member), value);
            }
            return Db.SortedSetIncrementAsync(GetKey(key), member.ToString(), value);
        }

        /// <summary>
        /// 获取排名的成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public async Task<IList<T>> SortedSetRangeByRankAsync<T>(string key, long start = 0, long stop = -1)
        {
            var list = new List<T>();
            var redisValues = await Db.SortedSetRangeByRankAsync(GetKey(key), start, stop);
            if (redisValues.Any())
            {
                if (IsNotBaseType<T>())
                {
                    foreach (var redisValue in redisValues)
                    {
                        list.Add(JsonSerializer.Deserialize<T>(redisValue));
                    }
                }
                else
                {
                    foreach (var redisValue in redisValues)
                    {
                        list.Add(redisValue.To<T>());
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取排名的成员和值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public async Task<IDictionary<T, double>> SortedSetRangeByRankWithScoresAsync<T>(string key, long start = 0, long stop = -1)
        {
            var dic = new Dictionary<T, double>();
            var redisValues = await Db.SortedSetRangeByRankWithScoresAsync(GetKey(key), start, stop);
            if (redisValues.Any())
            {
                if (IsNotBaseType<T>())
                {
                    foreach (var redisValue in redisValues)
                    {
                        dic.Add(JsonSerializer.Deserialize<T>(redisValue.Element), redisValue.Score);
                    }
                }
                else
                {
                    foreach (var redisValue in redisValues)
                    {
                        dic.Add(redisValue.Element.To<T>(), redisValue.Score);
                    }
                }
            }
            return dic;
        }

        /// <summary>
        /// 返回指定分数区间的成员数量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public Task<long> SortedSetLengthAsync(string key, double min = double.NegativeInfinity, double max = double.PositiveInfinity)
        {
            return Db.SortedSetLengthAsync(GetKey(key), min, max);
        }

        /// <summary>
        /// 删除并返回第一个元素，默认情况下，分数是从低到高排序的。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<KeyValuePair<T, double>> SortedSetLengthAsync<T>(string key, Order order = Order.Ascending)
        {
            var entry = await Db.SortedSetPopAsync(GetKey(key), order);
            if (entry != null)
            {
                if (IsNotBaseType<T>())
                {
                    return new KeyValuePair<T, double>(JsonSerializer.Deserialize<T>(entry.Value.Element), entry.Value.Score);
                }

                return new KeyValuePair<T, double>(entry.Value.Element.To<T>(), entry.Value.Score);
            }

            return new KeyValuePair<T, double>();
        }

        #endregion

        #region ==KeyDelete==

        /// <summary>
        /// 删除键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyDelete(string key)
        {
            return Db.KeyDelete(GetKey(key));
        }

        /// <summary>
        /// 删除键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> KeyDeleteAsync(string key)
        {
            return Db.KeyDeleteAsync(GetKey(key));
        }

        #endregion

        #region ==KeyExists==

        /// <summary>
        /// 是否存在键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            return Db.KeyExists(GetKey(key));
        }

        /// <summary>
        /// 是否存在键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> KeyExistsAsync(string key)
        {
            return Db.KeyExistsAsync(GetKey(key));
        }

        #endregion

        #region ==KeyExpire==

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool KeyExpire(string key, DateTime? expiry)
        {
            return Db.KeyExpire(GetKey(key), expiry);
        }

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public Task<bool> KeyExpireAsync(string key, DateTime? expiry)
        {
            return Db.KeyExpireAsync(GetKey(key), expiry);
        }

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool KeyExpire(string key, TimeSpan? expiry)
        {
            return Db.KeyExpire(GetKey(key), expiry);
        }

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public Task<bool> KeyExpireAsync(string key, TimeSpan? expiry)
        {
            return Db.KeyExpireAsync(GetKey(key), expiry);
        }

        #endregion

        #region ==Other==

        /// <summary>
        /// 分页获取所有Keys
        /// </summary>
        /// <param name="database"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageOffset"></param>
        /// <returns></returns>
        public IEnumerable<RedisKey> GetAllKeys(int database = 0, int pageSize = 10, int pageOffset = 0)
        {
            return _redis.GetServer(_redis.GetEndPoints()[0]).Keys(database, pageSize: pageSize, pageOffset: pageOffset);
        }

        /// <summary>
        /// 分页获取指定前缀的Keys列表
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="database"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageOffset"></param>
        /// <returns></returns>
        public IEnumerable<RedisKey> GetKeysByPrefix(string prefix, int database = 0, int pageSize = 10, int pageOffset = 0)
        {
            if (prefix.IsNull())
                return null;

            return _redis.GetServer(_redis.GetEndPoints()[0]).Keys(database, $"{prefix}*", pageSize, pageOffset);
        }

        /// <summary>
        /// 删除指定前缀的Keys
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public async Task DeleteByPrefix(string prefix)
        {
            var pageSize = 1000;
            var pageOffset = 0;
            var hasEnd = false;
            while (!hasEnd)
            {
                var keys = GetKeysByPrefix(prefix, 0, pageSize, pageOffset);
                if (keys == null || !keys.Any())
                {
                    hasEnd = true;
                }
                else
                {
                    await Db.KeyDeleteAsync(keys.ToArray());
                }
            }
        }

        public void Dispose()
        {
            _redis?.Dispose();
        }

        /// <summary>
        /// 是否是基础类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private bool IsNotBaseType<T>()
        {
            var type = typeof(T);

            return type != typeof(Guid) && Type.GetTypeCode(type) == TypeCode.Object;
        }

        #endregion
    }
}
