namespace NetModular.Lib.MQ.RabbitMQ
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// 配置项
    /// </summary>
    public class RabbitMQConfig
    {
        /// <summary>
        /// 主机名
        /// </summary>
        public string HostName { get; set; } = "localhost";

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; } = 5672;

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = "guest";

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = "guest";

        /// <summary>
        /// 虚拟目录
        /// </summary>
        public string VirtualHost { get; set; }

        /// <summary>
        /// 队列名称前缀
        /// </summary>
        public string Prefix { get; set; }
    }
}
