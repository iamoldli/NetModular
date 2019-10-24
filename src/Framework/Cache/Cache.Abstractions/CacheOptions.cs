namespace Nm.Lib.Cache.Abstractions
{
    /// <summary>
    /// 缓存配置项
    /// </summary>
    public class CacheOptions
    {
        /// <summary>
        /// 缓存方式
        /// </summary>
        public CacheMode Mode { get; set; }

        /// <summary>
        /// Redis配置项
        /// </summary>
        public RedisOptions Redis { get; set; }
    }

    public class RedisOptions
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
