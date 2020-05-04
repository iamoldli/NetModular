namespace NetModular.Lib.Cache.Abstractions
{
    /// <summary>
    /// 缓存配置
    /// </summary>
    public class CacheConfig
    {
        /// <summary>
        /// 缓存提供器
        /// </summary>
        public CacheProvider Provider { get; set; } = CacheProvider.MemoryCache;

        /// <summary>
        /// Redis配置
        /// </summary>
        public RedisConfig Redis { get; set; } = new RedisConfig();
    }

    public class RedisConfig
    {
        /// <summary>
        /// 前缀，用于区分不用的项目
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; } = "127.0.0.1";
    }
}
