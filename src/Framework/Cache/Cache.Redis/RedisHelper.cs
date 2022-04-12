using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Cache.Abstractions;
using StackExchange.Redis;
using NetModular.Lib.Utils.Core.Attributes;

namespace NetModular.Lib.Cache.Redis
{
    [Singleton]
    public class RedisHelper : IDisposable
    {
        internal ConnectionMultiplexer _redis;
        internal string _prefix;
        internal readonly RedisConfig _config;
        internal readonly IRedisSerializer _redisSerializer;
        public IDatabase Db;
        public RedisDatabase Database;

        public RedisHelper(CacheConfig config, IRedisSerializer redisSerializer)
        {
            _redisSerializer = redisSerializer;
            _config = config.Redis;
            CreateConnection();
        }

        /// <summary>
        /// 获取Redis原生的IDatabase
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public IDatabase GetDb(int db = -1)
        {
            if (db == -1)
                db = _config.DefaultDb;

            return _redis.GetDatabase(db);
        }

        /// <summary>
        /// 获取自定义的RedisDatabase
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public RedisDatabase GetDatabase(int db = -1)
        {
            if (db == -1)
                db = _config.DefaultDb;

            return new RedisDatabase(db, this);
        }

        /// <summary>
        /// 获取键(附加前缀)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetKey(string key)
        {
            return $"{_prefix}{key}";
        }

        /// <summary>
        /// 创建连接
        /// </summary>
        internal void CreateConnection()
        {
            _prefix = _config.Prefix;
            _redis = ConnectionMultiplexer.Connect(_config.ConnectionString);
            Db = GetDb();
            Database = GetDatabase();
        }

        /// <summary>
        /// 获取Redis连接对象
        /// </summary>
        public ConnectionMultiplexer Conn
        {
            get
            {
                if (_redis == null)
                {
                    CreateConnection();
                }

                return _redis;
            }
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
            return Database.StringSetAsync(key, obj, expiry);
        }

        /// <summary>
        /// 获取字符串类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<T> StringGetAsync<T>(string key)
        {
            return Database.StringGetAsync<T>(key);
        }

        /// <summary>
        /// 字符串减去数值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> StringDecrementAsync(string key, long value = 1)
        {
            return Database.StringDecrementAsync(key, value);
        }

        /// <summary>
        /// 字符串增加数值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> StringIncrementAsync(string key, long value = 1)
        {
            return Database.StringIncrementAsync(key, value);
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
            return Database.HashSetAsync(key, field, obj);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public Task<T> HashGetAsync<T>(string key, string field)
        {
            return Database.HashGetAsync<T>(key, field);
        }

        /// <summary>
        /// 获取所有值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<IList<T>> HashValuesAsync<T>(string key)
        {
            return Database.HashValuesAsync<T>(key);
        }

        /// <summary>
        /// 删除值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public Task<bool> HashDeleteAsync(string key, string field)
        {
            return Database.HashDeleteAsync(key, field);
        }

        /// <summary>
        /// 获取所有键值集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<IList<KeyValuePair<string, T>>> HashGetAllAsync<T>(string key)
        {
            return Database.HashGetAllAsync<T>(key);
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
            return Database.HashDecrementAsync(key, field, value);
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
            return Database.HashIncrementAsync(key, field, value);
        }

        /// <summary>
        /// 判断字段是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public Task<bool> HashExistsAsync(string key, string field)
        {
            return Database.HashExistsAsync(key, field);
        }

        #endregion

        #region ==Set==

        public Task<bool> SetAddAsync<T>(string key, T obj)
        {
            return Database.SetAddAsync(key, obj);
        }

        public Task<bool> SetRemoveAsync<T>(string key, T obj)
        {
            return Database.SetRemoveAsync(key, obj);
        }

        public Task<bool> SetContainsAsync<T>(string key, T obj)
        {
            return Database.SetContainsAsync(key, obj);
        }

        public Task<long> SetLengthAsync(string key)
        {
            return Database.SetLengthAsync(key);
        }

        public Task<T> SetPopAsync<T>(string key)
        {
            return Database.SetPopAsync<T>(key);
        }

        public Task<IList<T>> SetMembersAsync<T>(string key)
        {
            return Database.SetMembersAsync<T>(key);
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
            return Database.SortedSetAddAsync(key, member, score);
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
            return Database.SortedSetDecrementAsync(key, member, value);
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
            return Database.SortedSetIncrementAsync(key, member, value);
        }

        /// <summary>
        /// 获取排名的成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public Task<IList<T>> SortedSetRangeByRankAsync<T>(string key, long start = 0, long stop = -1, Order order = Order.Ascending)
        {
            return Database.SortedSetRangeByRankAsync<T>(key, start, stop, order);
        }

        /// <summary>
        /// 获取排名的成员和值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public Task<IList<KeyValuePair<T, double>>> SortedSetRangeByRankWithScoresAsync<T>(string key, long start = 0, long stop = -1, Order order = Order.Ascending)
        {
            return Database.SortedSetRangeByRankWithScoresAsync<T>(key, start, stop, order);
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
            return Database.SortedSetLengthAsync(key, min, max);
        }

        /// <summary>
        /// 删除并返回第一个元素，默认情况下，分数是从低到高排序的。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public Task<KeyValuePair<T, double>> SortedSetPopAsync<T>(string key, Order order = Order.Ascending)
        {
            return Database.SortedSetPopAsync<T>(key, order);
        }

        /// <summary>
        /// 删除指定成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public Task<bool> SortedSetRemoveAsync<T>(string key, T member)
        {
            return Database.SortedSetRemoveAsync(key, member);
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
            return Database.SortedSetRemoveRangeByScoreAsync(key, start, stop);
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
            return Database.SortedSetRemoveRangeByRankAsync(key, start, stop);
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
            return Database.KeyDelete(key);
        }

        /// <summary>
        /// 删除键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> KeyDeleteAsync(string key)
        {
            return Database.KeyDeleteAsync(key);
        }

        /// <summary>
        /// 删除键
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public long KeyDelete(string[] keys)
        {
            return Database.KeyDelete(keys);
        }

        /// <summary>
        /// 删除键
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public Task<long> KeyDeleteAsync(string[] keys)
        {
            return Database.KeyDeleteAsync(keys);
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
            return Database.KeyExists(key);
        }

        /// <summary>
        /// 是否存在键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> KeyExistsAsync(string key)
        {
            return Database.KeyExistsAsync(key);
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
            return Database.KeyExpire(key, expiry);
        }

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public Task<bool> KeyExpireAsync(string key, DateTime? expiry)
        {
            return Database.KeyExpireAsync(key, expiry);
        }

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool KeyExpire(string key, TimeSpan? expiry)
        {
            return Database.KeyExpire(key, expiry);
        }

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public Task<bool> KeyExpireAsync(string key, TimeSpan? expiry)
        {
            return Database.KeyExpireAsync(key, expiry);
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

            return _redis.GetServer(_redis.GetEndPoints()[0]).Keys(database, $"{GetKey(prefix)}*", pageSize, pageOffset).ToList();
        }

        /// <summary>
        /// 删除指定前缀的Keys
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public Task DeleteByPrefix(string prefix)
        {
            return Database.DeleteByPrefix(prefix);
        }

        public void Dispose()
        {
            _redis?.Dispose();
        }

        #endregion
    }
}
