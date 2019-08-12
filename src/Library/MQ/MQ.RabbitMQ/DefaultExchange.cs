namespace Tm.Lib.MQ.RabbitMQ
{
    /// <summary>
    /// 默认交换器
    /// </summary>
    public class DefaultExchange
    {
        public const string Direct = "Tm.direct";

        public const string Fanout = "Tm.fanout";

        public const string Topic = "Tm.topic";

        public const string Headers = "Tm.headers";
    }
}
