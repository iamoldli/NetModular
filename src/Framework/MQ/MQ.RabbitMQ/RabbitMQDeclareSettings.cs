using System.Collections.Generic;
using RabbitMQ.Client;

namespace NetModular.Lib.MQ.RabbitMQ
{
    /// <summary>
    /// 队列定义设置
    /// </summary>
    public class RabbitMQDeclareSettings
    {
        /// <summary>
        /// 交换器设置
        /// </summary>
        public ExchangeDeclareSettings Exchange { get; set; } = new ExchangeDeclareSettings();

        /// <summary>
        /// 队列设置
        /// </summary>
        public QueueDeclareSettings Queue { get; set; } = new QueueDeclareSettings();
    }

    /// <summary>
    /// 交换器定义设置
    /// </summary>
    public class ExchangeDeclareSettings
    {
        /// <summary>
        /// 交换器名称
        /// </summary>
        public string Name { get; set; } = DefaultExchange.Direct;

        /// <summary>
        /// 交换器类型
        /// </summary>
        public string Type { get; set; } = ExchangeType.Direct;

        /// <summary>
        /// 是否持久化
        /// </summary>
        public bool Durable { get; set; } = true;

        /// <summary>
        /// 自动删除
        /// </summary>
        public bool AutoDelete { get; set; }

        /// <summary>
        /// 交换器
        /// </summary>
        public IDictionary<string, object> Arguments { get; set; }
    }

    /// <summary>
    /// 队列定义设置
    /// </summary>
    public class QueueDeclareSettings
    {
        /// <summary>
        /// 是否持久化
        /// </summary>
        public bool Durable { get; set; } = true;

        /// <summary>
        /// 是否排他
        /// </summary>
        public bool Exclusive { get; set; }

        /// <summary>
        /// 自动删除
        /// </summary>
        public bool AutoDelete { get; set; }

        /// <summary>
        /// 交换器
        /// </summary>
        public IDictionary<string, object> Arguments { get; set; }
    }
}
