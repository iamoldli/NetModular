using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;
using NetModular.Lib.Cache.Abstractions;
using Newtonsoft.Json;

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
            return $"{_prefix}:{key}";
        }

        #region ==String==

        public Task<bool> StringSetAsync<T>(string key, T obj, TimeSpan? expiry = null)
        {
            return Db.StringSetAsync(GetKey(key), Serialize(obj), expiry);
        }

        public async Task<T> StringGetAsync<T>(string key)
        {
            var cache = await Db.StringGetAsync(GetKey(key));
            return cache.HasValue ? Deserialize<T>(cache) : default;
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
        public Task<bool> HashSetAsync<T>(string key, string field, T obj)
        {
            return Db.HashSetAsync(GetKey(key), field, Serialize(obj));
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
            return cache.HasValue ? Deserialize<T>(cache) : default;
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
            return cache.Any() ? cache.Select(Deserialize<T>).ToList() : default;
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
        public async Task<IList<KeyValuePair<string, T>>> HashGetAllAsync<T>(string key)
        {
            var cache = await Db.HashGetAllAsync(GetKey(key));
            return cache.Select(m => new KeyValuePair<string, T>(m.Name.ToString(), Deserialize<T>(m.Value))).ToList();
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
        public Task<long> HashIncrementAsync(string key, string field, long value = 1)
        {
            return Db.HashIncrementAsync(GetKey(key), field, value);
        }

        /// <summary>
        /// 判断字段是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public Task<bool> HashExistsAsync(string key, string field)
        {
            return Db.HashExistsAsync(GetKey(key), field);
        }

        #endregion

        #region ==Set==

        public Task<bool> SetAddAsync<T>(string key, T obj)
        {
            return Db.SetAddAsync(GetKey(key), Serialize(obj));
        }

        public Task<bool> SetRemoveAsync<T>(string key, T obj)
        {
            return Db.SetRemoveAsync(GetKey(key), Serialize(obj));
        }

        public Task<bool> SetContainsAsync<T>(string key, T obj)
        {
            return Db.SetContainsAsync(GetKey(key), Serialize(obj));
        }

        public Task<long> SetLengthAsync(string key)
        {
            return Db.SetLengthAsync(GetKey(key));
        }

        public async Task<T> SetPopAsync<T>(string key)
        {
            var cache = await Db.SetPopAsync(GetKey(key));
            return cache.HasValue ? Deserialize<T>(cache) : default;
        }

        public async Task<IList<T>> SetMembersAsync<T>(string key)
        {
            var cache = await Db.SetMembersAsync(GetKey(key));
            return cache.Any() ? cache.Select(Deserialize<T>).ToList() : default;
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
            return Db.SortedSetAddAsync(GetKey(key), Serialize(member), score);
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
            return Db.SortedSetDecrementAsync(GetKey(key), Serialize(member), value);
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
            return Db.SortedSetIncrementAsync(GetKey(key), Serialize(member), value);
        }

        /// <summary>
        /// 获取排名的成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<IList<T>> SortedSetRangeByRankAsync<T>(string key, long start = 0, long stop = -1, Order order = Order.Ascending)
        {
            var cache = await Db.SortedSetRangeByRankAsync(GetKey(key), start, stop, order);
            return cache.Select(Deserialize<T>).ToList();
        }

        /// <summary>
        /// 获取排名的成员和值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<IList<KeyValuePair<T, double>>> SortedSetRangeByRankWithScoresAsync<T>(string key, long start = 0, long stop = -1, Order order = Order.Ascending)
        {
            var cache = await Db.SortedSetRangeByRankWithScoresAsync(GetKey(key), start, stop, order);
            return cache.Select(m => new KeyValuePair<T, double>(Deserialize<T>(m.Element), m.Score)).ToList();
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
            return entry != null ? new KeyValuePair<T, double>(Deserialize<T>(entry.Value.Element), entry.Value.Score) : default;
        }

        /// <summary>
        /// 删除指定成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public Task<bool> SortedSetRemoveAsync<T>(string key, T member)
        {
            return Db.SortedSetRemoveAsync(GetKey(key), Serialize(member));
        }

        /// <summary>
        /// 根据分数区间删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns>删除数量</returns>
        public Task<long> SortedSetRemoveRangeByScoreAsync<T>(string key, long start = 0, long stop = -1)
        {
            return Db.SortedSetRemoveRangeByScoreAsync(GetKey(key), start, stop);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns>删除数量</returns>
        public Task<long> SortedSetRemoveRangeByRankAsync<T>(string key, long start = 0, long stop = -1)
        {
            return Db.SortedSetRemoveRangeByRankAsync(GetKey(key), start, stop);
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
        public IList<RedisKey> GetAllKeys(int database = 0, int pageSize = 10, int pageOffset = 0)
        {
            return _redis.GetServer(_redis.GetEndPoints()[0]).Keys(database, pageSize: pageSize, pageOffset: pageOffset).ToList();
        }

        /// <summary>
        /// 分页获取指定前缀的Keys列表
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="database"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageOffset"></param>
        /// <returns></returns>
        public IList<RedisKey> GetKeysByPrefix(string prefix, int database = 0, int pageSize = 10, int pageOffset = 0)
        {
            if (prefix.IsNull())
                return null;

            return _redis.GetServer(_redis.GetEndPoints()[0]).Keys(database, $"{prefix}*", pageSize, pageOffset).ToList();
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
            return Type.GetTypeCode(typeof(T)) == TypeCode.Object;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        private string Serialize<T>(T value)
        {
            if (IsNotBaseType<T>())
            {
                return JsonConvert.SerializeObject(value);
            }

            return value.ToString();
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        private T Deserialize<T>(RedisValue value)
        {
            if (IsNotBaseType<T>())
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return value.To<T>();
        }

        #endregion
    }
}
