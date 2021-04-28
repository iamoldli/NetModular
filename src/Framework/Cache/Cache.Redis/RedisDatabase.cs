using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace NetModular.Lib.Cache.Redis
{
    /// <summary>
    /// 自定义的Redis数据库
    /// </summary>
    public class RedisDatabase
    {
        private readonly IDatabase _db;
        private readonly RedisHelper _redisHelper;
        private readonly ConnectionMultiplexer _redis;
        private readonly IRedisSerializer _redisSerializer;
        private readonly int _dbIndex;

        public RedisDatabase(int dbIndex, RedisHelper redisHelper)
        {
            _dbIndex = dbIndex < 0 ? 0 : dbIndex;
            _redisHelper = redisHelper;
            _redis = redisHelper._redis;
            _redisSerializer = redisHelper._redisSerializer;
            _db = _redis.GetDatabase(_dbIndex);
        }

        private string GetKey(string key)
        {
            return _redisHelper.GetKey(key);
        }

        #region ==String==

        /// <summary>
        /// 写入字符串类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public Task<bool> StringSetAsync<T>(string key, T obj, TimeSpan? expiry = null)
        {
            return _db.StringSetAsync(GetKey(key), _redisSerializer.Serialize(obj), expiry);
        }

        /// <summary>
        /// 获取字符串类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> StringGetAsync<T>(string key)
        {
            var cache = await _db.StringGetAsync(GetKey(key));
            return cache.HasValue ? _redisSerializer.Deserialize<T>(cache) : default;
        }

        /// <summary>
        /// 字符串减去数值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> StringDecrementAsync(string key, long value = 1)
        {
            return _db.StringDecrementAsync(GetKey(key), value);
        }

        /// <summary>
        /// 字符串增加数值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> StringIncrementAsync(string key, long value = 1)
        {
            return _db.StringIncrementAsync(GetKey(key), value);
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
            return _db.HashSetAsync(GetKey(key), field, _redisSerializer.Serialize(obj));
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
            var cache = await _db.HashGetAsync(GetKey(key), field);
            return cache.HasValue ? _redisSerializer.Deserialize<T>(cache) : default;
        }

        /// <summary>
        /// 获取所有值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IList<T>> HashValuesAsync<T>(string key)
        {
            var cache = await _db.HashValuesAsync(GetKey(key));
            return cache.Any() ? cache.Select(_redisSerializer.Deserialize<T>).ToList() : default;
        }

        /// <summary>
        /// 删除值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public Task<bool> HashDeleteAsync(string key, string field)
        {
            return _db.HashDeleteAsync(GetKey(key), field);
        }

        /// <summary>
        /// 获取所有键值集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IList<KeyValuePair<string, T>>> HashGetAllAsync<T>(string key)
        {
            var cache = await _db.HashGetAllAsync(GetKey(key));
            return cache.Select(m => new KeyValuePair<string, T>(m.Name.ToString(), _redisSerializer.Deserialize<T>(m.Value))).ToList();
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
            return _db.HashDecrementAsync(GetKey(key), field, value);
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
            return _db.HashIncrementAsync(GetKey(key), field, value);
        }

        /// <summary>
        /// 判断字段是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public Task<bool> HashExistsAsync(string key, string field)
        {
            return _db.HashExistsAsync(GetKey(key), field);
        }

        #endregion

        #region ==Set==

        public Task<bool> SetAddAsync<T>(string key, T obj)
        {
            return _db.SetAddAsync(GetKey(key), _redisSerializer.Serialize(obj));
        }

        public Task<bool> SetRemoveAsync<T>(string key, T obj)
        {
            return _db.SetRemoveAsync(GetKey(key), _redisSerializer.Serialize(obj));
        }

        public Task<bool> SetContainsAsync<T>(string key, T obj)
        {
            return _db.SetContainsAsync(GetKey(key), _redisSerializer.Serialize(obj));
        }

        public Task<long> SetLengthAsync(string key)
        {
            return _db.SetLengthAsync(GetKey(key));
        }

        public async Task<T> SetPopAsync<T>(string key)
        {
            var cache = await _db.SetPopAsync(GetKey(key));
            return cache.HasValue ? _redisSerializer.Deserialize<T>(cache) : default;
        }

        public async Task<IList<T>> SetMembersAsync<T>(string key)
        {
            var cache = await _db.SetMembersAsync(GetKey(key));
            return cache.Any() ? cache.Select(_redisSerializer.Deserialize<T>).ToList() : default;
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
            return _db.SortedSetAddAsync(GetKey(key), _redisSerializer.Serialize(member), score);
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
            return _db.SortedSetDecrementAsync(GetKey(key), _redisSerializer.Serialize(member), value);
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
            return _db.SortedSetIncrementAsync(GetKey(key), _redisSerializer.Serialize(member), value);
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
            var cache = await _db.SortedSetRangeByRankAsync(GetKey(key), start, stop, order);
            return cache.Select(_redisSerializer.Deserialize<T>).ToList();
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
            var cache = await _db.SortedSetRangeByRankWithScoresAsync(GetKey(key), start, stop, order);
            return cache.Select(m => new KeyValuePair<T, double>(_redisSerializer.Deserialize<T>(m.Element), m.Score)).ToList();
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
            return _db.SortedSetLengthAsync(GetKey(key), min, max);
        }

        /// <summary>
        /// 删除并返回第一个元素，默认情况下，分数是从低到高排序的。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<KeyValuePair<T, double>> SortedSetPopAsync<T>(string key, Order order = Order.Ascending)
        {
            var entry = await _db.SortedSetPopAsync(GetKey(key), order);
            return entry != null ? new KeyValuePair<T, double>(_redisSerializer.Deserialize<T>(entry.Value.Element), entry.Value.Score) : default;
        }

        /// <summary>
        /// 删除指定成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public Task<bool> SortedSetRemoveAsync<T>(string key, T member)
        {
            return _db.SortedSetRemoveAsync(GetKey(key), _redisSerializer.Serialize(member));
        }

        /// <summary>
        /// 根据分数区间删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns>删除数量</returns>
        public Task<long> SortedSetRemoveRangeByScoreAsync(string key, long start = 0, long stop = -1)
        {
            return _db.SortedSetRemoveRangeByScoreAsync(GetKey(key), start, stop);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns>删除数量</returns>
        public Task<long> SortedSetRemoveRangeByRankAsync(string key, long start = 0, long stop = -1)
        {
            return _db.SortedSetRemoveRangeByRankAsync(GetKey(key), start, stop);
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
            return _db.KeyDelete(GetKey(key));
        }

        /// <summary>
        /// 删除键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> KeyDeleteAsync(string key)
        {
            return _db.KeyDeleteAsync(GetKey(key));
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
            return _db.KeyExists(GetKey(key));
        }

        /// <summary>
        /// 是否存在键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> KeyExistsAsync(string key)
        {
            return _db.KeyExistsAsync(GetKey(key));
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
            return _db.KeyExpire(GetKey(key), expiry);
        }

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public Task<bool> KeyExpireAsync(string key, DateTime? expiry)
        {
            return _db.KeyExpireAsync(GetKey(key), expiry);
        }

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool KeyExpire(string key, TimeSpan? expiry)
        {
            return _db.KeyExpire(GetKey(key), expiry);
        }

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public Task<bool> KeyExpireAsync(string key, TimeSpan? expiry)
        {
            return _db.KeyExpireAsync(GetKey(key), expiry);
        }

        #endregion

        #region ==Other==

        /// <summary>
        /// 分页获取所有Keys
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageOffset"></param>
        /// <returns></returns>
        public IList<RedisKey> GetAllKeys(int pageSize = 10, int pageOffset = 0)
        {
            return _redis.GetServer(_redis.GetEndPoints()[0]).Keys(_dbIndex, pageSize: pageSize, pageOffset: pageOffset).ToList();
        }

        /// <summary>
        /// 分页获取指定前缀的Keys列表
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageOffset"></param>
        /// <returns></returns>
        public IList<RedisKey> GetKeysByPrefix(string prefix, int pageSize = 10, int pageOffset = 0)
        {
            if (prefix.IsNull())
                return null;

            return _redis.GetServer(_redis.GetEndPoints()[0]).Keys(_dbIndex, $"{GetKey(prefix)}*", pageSize, pageOffset).ToList();
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
                var keys = GetKeysByPrefix(prefix, pageSize, pageOffset);
                if (keys == null || !keys.Any())
                {
                    hasEnd = true;
                }
                else
                {
                    var tasks = new List<Task<bool>>();
                    keys.ToList().ForEach(m => tasks.Add(_db.KeyDeleteAsync(m)));
                    await Task.WhenAll(tasks);
                }
            }
        }

        #endregion
    }
}
