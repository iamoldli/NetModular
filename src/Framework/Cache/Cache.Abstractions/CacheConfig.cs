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
        private int _defaultDb;

        /// <summary>
        /// 默认数据库
        /// </summary>
        public int DefaultDb
        {
            get => _defaultDb < 0 ? 0 : _defaultDb;
            set => _defaultDb = value;
        }

        private string _prefix;

        /// <summary>
        /// 前缀，用于区分不用的项目
        /// </summary>
        public string Prefix
        {
            get => _prefix.NotNull() ? $"{_prefix}:" : string.Empty;
            set => _prefix = value;
        }

        private string _connnectionString = "127.0.0.1";
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get => _connnectionString.NotNull() ? _connnectionString : "127.0.0.1";
            set => _connnectionString = value;
        }

    }
}
