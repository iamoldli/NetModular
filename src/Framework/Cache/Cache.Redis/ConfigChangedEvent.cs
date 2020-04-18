using System;
using NetModular.Lib.Config.Abstractions;

namespace NetModular.Lib.Cache.Redis
{
    public class ConfigChangedEvent : IConfigChangeEvent<RedisConfig>
    {
        private readonly RedisHelper _redisHelper;

        public ConfigChangedEvent(RedisHelper redisHelper)
        {
            _redisHelper = redisHelper;
        }

        public void OnChanged(RedisConfig newConfig, RedisConfig oldConfig)
        {
            _redisHelper.CreateConnection();
        }
    }
}
