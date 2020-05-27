using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NetModular.Lib.MQ.RabbitMQ
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// RabbitMQ客户端
    /// </summary>
    public class RabbitMQClient : IDisposable
    {
        //发送连接
        private IConnection _sendConnection;

        //接收连接
        private IConnection _receiveConnection;

        private readonly RabbitMQConfig _config;
        public RabbitMQClient(RabbitMQConfig config)
        {
            _config = config;

            CreateConnection();
        }

        internal void CreateConnection()
        {
            Check.NotNull(_config.UserName, nameof(_config.UserName), "用户名不能为空");
            Check.NotNull(_config.Password, nameof(_config.Password), "密码不能为空");

            if (_config.HostName.IsNull())
                _config.HostName = "localhost";

            if (_config.Port < 1 || _config.Port > 65535)
                _config.Port = 5672;

            var factory = new ConnectionFactory
            {
                HostName = _config.HostName,
                Port = _config.Port,
                UserName = _config.UserName,
                Password = _config.Password,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
            };

            if (_config.VirtualHost.NotNull())
                factory.VirtualHost = _config.VirtualHost;

            _sendConnection = factory.CreateConnection();

            _receiveConnection = factory.CreateConnection();
        }

        /// <summary>
        /// 发送单条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue"></param>
        /// <param name="routingKey"></param>
        /// <param name="message"></param>
        /// <param name="exchange"></param>
        /// <param name="exchangeType"></param>
        public void Send<T>(string queue, T message, string routingKey = "", string exchange = DefaultExchange.Direct, string exchangeType = ExchangeType.Direct)
        {
            Check.NotNull(queue, nameof(queue), "queue is null");

            queue = GetQueueName(queue);

            if (routingKey.IsNull())
                routingKey = queue;

            using var channel = _sendConnection.CreateModel();
            if (exchange.IsNull())
            {
                switch (exchangeType)
                {
                    case ExchangeType.Fanout:
                        exchange = DefaultExchange.Fanout;
                        break;
                    case ExchangeType.Topic:
                        exchange = DefaultExchange.Topic;
                        break;
                    case ExchangeType.Headers:
                        exchange = DefaultExchange.Headers;
                        break;
                    default:
                        exchange = DefaultExchange.Direct;
                        break;
                }
            }

            channel.ExchangeDeclare(exchange, exchangeType, true, false, null);
            channel.QueueDeclare(queue, true, false, false);
            channel.QueueBind(queue, exchange, routingKey);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange, routingKey, properties, body);
        }

        /// <summary>
        /// 使用事件接收消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue"></param>
        /// <param name="func"></param>
        public Consumer Receive<T>(string queue, Func<T, bool> func)
        {
            Check.NotNull(queue, nameof(queue), "queue is null");
            Check.NotNull(func, nameof(func), "func is null");

            queue = GetQueueName(queue);

            var channel = _receiveConnection.CreateModel();
            channel.BasicQos(0, 1, false);
            channel.QueueDeclare(queue, true, false, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, eventArgs) =>
            {
                var message = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(eventArgs.Body));
                if (func(message))
                {
                    channel.BasicAck(eventArgs.DeliveryTag, false);
                }
                else
                {
                    channel.BasicNack(eventArgs.DeliveryTag, false, true);
                }
            };

            var tag = channel.BasicConsume(queue, false, consumer);
            return new Consumer
            {
                Channel = channel,
                Tag = tag
            };
        }

        public void Dispose()
        {
            _sendConnection?.Dispose();
            _receiveConnection?.Dispose();
        }

        private string GetQueueName(string queue)
        {
            return _config.Prefix.NotNull() ? $"{_config.Prefix}.{queue}" : queue;
        }
    }
}
