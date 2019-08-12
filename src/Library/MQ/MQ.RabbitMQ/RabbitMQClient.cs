using System;
using System.Text;
using Newtonsoft.Json;
using Tm.Lib.Utils.Core;
using Tm.Lib.Utils.Core.Extensions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Tm.Lib.MQ.RabbitMQ
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// RabbitMQ客户端
    /// </summary>
    public class RabbitMQClient : IDisposable
    {
        //发送连接
        private readonly IConnection _sendConnection;

        //接收连接
        private readonly IConnection _receiveConnection;

        public RabbitMQClient(RabbitMQOptions options)
        {
            Check.NotNull(options.UserName, nameof(options.UserName), "用户名不能为空");
            Check.NotNull(options.Password, nameof(options.Password), "密码不能为空");

            if (options.HostName.IsNull())
                options.HostName = "localhost";

            if (options.Port < 1 || options.Port > 65535)
                options.Port = 5672;

            var factory = new ConnectionFactory
            {
                HostName = options.HostName,
                Port = options.Port,
                UserName = options.UserName,
                Password = options.Password,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
            };

            if (options.virtualHost.NotNull())
                factory.VirtualHost = options.virtualHost;

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

            if (routingKey.IsNull())
                routingKey = queue;

            using (var channel = _sendConnection.CreateModel())
            {
                if (exchange.IsNull())
                {
                    switch (exchangeType)
                    {
                        case ExchangeType.Direct:
                            exchange = DefaultExchange.Direct;
                            break;
                        case ExchangeType.Fanout:
                            exchange = DefaultExchange.Fanout;
                            break;
                        case ExchangeType.Topic:
                            exchange = DefaultExchange.Topic;
                            break;
                        case ExchangeType.Headers:
                            exchange = DefaultExchange.Headers;
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
    }
}
