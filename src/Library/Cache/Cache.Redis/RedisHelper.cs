using Nm.Lib.Utils.Core;
using StackExchange.Redis;

namespace Nm.Lib.Cache.Redis
{
    public class RedisHelper
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;
        private readonly RedisOptions _options;

        public RedisHelper(RedisOptions options)
        {
            Check.NotNull(options.ConnectionString, );

            _options = options;

            _redis = ConnectionMultiplexer.Connect(_options.ConnectionString);
            _db = _redis.GetDatabase();
        }
    }
}
