using NetModular.Lib.Config.Abstractions;

namespace NetModular.Lib.Cache.Redis
{
    /// <summary>
    /// Redis配置
    /// </summary>
    public class RedisConfig : IConfig
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
