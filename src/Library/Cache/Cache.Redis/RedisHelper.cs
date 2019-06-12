using Nm.Lib.Cache.Abstractions;
using Nm.Lib.Utils.Core;
using StackExchange.Redis;

namespace Nm.Lib.Cache.Redis
{
    public class RedisHelper
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public RedisHelper(RedisOptions options)
        {
            Check.NotNull(options.ConnectionString, nameof(RedisOptions), "未设置Redis连接信息");

            _redis = ConnectionMultiplexer.Connect(options.ConnectionString);
            _db = _redis.GetDatabase();
        }

        public IDatabase GetDb(int db = -1)
        {
            return _redis.GetDatabase(db);
        }
    }
}
