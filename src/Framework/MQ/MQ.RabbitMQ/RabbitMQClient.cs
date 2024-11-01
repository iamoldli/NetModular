using System;
using System.Text;
using System.Threading.Tasks;
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

            CreateConnectionAsync().GetAwaiter().GetResult();
        }

        internal async Task CreateConnectionAsync()
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

            _sendConnection = await factory.CreateConnectionAsync();

            _receiveConnection = await factory.CreateConnectionAsync();
        }

        /// <summary>
        /// 发送连接
        /// </summary>
        public IConnection SendConnection => _sendConnection;

        /// <summary>
        /// 接收连接
        /// </summary>
        public IConnection ReceiveConnection => _receiveConnection;

        /// <summary>
        /// 发送单条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue">队列名</param>
        /// <param name="routingKey">路由建</param>
        /// <param name="message">消息体</param>
        /// <param name="settings">配置</param>
        public async Task SendAsync<T>(string queue, T message, string routingKey = "", RabbitMQDeclareSettings settings = null)
        {
            Check.NotNull(queue, nameof(queue), "queue is null");

            queue = GetQueueName(queue);

            if (routingKey.IsNull())
                routingKey = queue;

            await using var channel = await _sendConnection.CreateChannelAsync();

            settings = GetSettings(settings);
            var exchange = settings.Exchange;
            await channel.ExchangeDeclareAsync(exchange.Name, exchange.Type, exchange.Durable, exchange.AutoDelete, exchange.Arguments);
            await channel.QueueDeclareAsync(queue, settings.Queue.Durable, settings.Queue.Exclusive, settings.Queue.AutoDelete, settings.Queue.Arguments);
            await channel.QueueBindAsync(queue, exchange.Name, routingKey);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            var properties = new BasicProperties
            {
                Persistent = true
            };

            await channel.BasicPublishAsync(exchange.Name, routingKey: routingKey, true, basicProperties: properties, body: body);
        }

        /// <summary>
        /// 使用事件接收消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue">队列名称</param>
        /// <param name="func">回调函数</param>
        /// <param name="settings">定义设置</param>
        public async Task<Consumer> Receive<T>(string queue, Func<T, bool> func, RabbitMQDeclareSettings settings = null)
        {
            Check.NotNull(queue, nameof(queue), "queue is null");
            Check.NotNull(func, nameof(func), "func is null");

            queue = GetQueueName(queue);

            var channel = await _receiveConnection.CreateChannelAsync();
            await channel.BasicQosAsync(0, 1, false);
            settings = GetSettings(settings);
            await channel.QueueDeclareAsync(queue, settings.Queue.Durable, settings.Queue.Exclusive, settings.Queue.AutoDelete, settings.Queue.Arguments);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (sender, eventArgs) =>
            {
                var message = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(eventArgs.Body.ToArray()));
                if (func(message))
                {
                    await channel.BasicAckAsync(eventArgs.DeliveryTag, false);
                }
                else
                {
                    await channel.BasicNackAsync(eventArgs.DeliveryTag, false, true);
                }
            };

            var tag = await channel.BasicConsumeAsync(queue, false, consumer);
            return new Consumer
            {
                Channel = channel,
                Tag = tag
            };
        }

        /// <summary>
        /// 使用事件接收消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue">队列名称</param>
        /// <param name="func">回调函数</param>
        /// <param name="settings">定义设置</param>
        public async Task<Consumer> Receive<T>(string queue, Func<T, string, bool> func, RabbitMQDeclareSettings settings = null)
        {
            Check.NotNull(queue, nameof(queue), "queue is null");
            Check.NotNull(func, nameof(func), "func is null");

            queue = GetQueueName(queue);

            var channel = await _receiveConnection.CreateChannelAsync();
            await channel.BasicQosAsync(0, 1, false);
            settings = GetSettings(settings);
            await channel.QueueDeclareAsync(queue, settings.Queue.Durable, settings.Queue.Exclusive, settings.Queue.AutoDelete, settings.Queue.Arguments);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (sender, eventArgs) =>
            {
                var message = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(eventArgs.Body.ToArray()));
                if (func(message, queue))
                {
                    await channel.BasicAckAsync(eventArgs.DeliveryTag, false);
                }
                else
                {
                    await channel.BasicNackAsync(eventArgs.DeliveryTag, false, true);
                }
            };

            var tag = await channel.BasicConsumeAsync(queue, false, consumer);
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

        private RabbitMQDeclareSettings GetSettings(RabbitMQDeclareSettings settings)
        {
            settings ??= new RabbitMQDeclareSettings();

            settings.Exchange ??= new ExchangeDeclareSettings();

            settings.Queue ??= new QueueDeclareSettings();

            var exchange = settings.Exchange;

            if (exchange.Name.IsNull())
            {
                switch (exchange.Type)
                {
                    case ExchangeType.Fanout:
                        exchange.Name = DefaultExchange.Fanout;
                        break;
                    case ExchangeType.Topic:
                        exchange.Name = DefaultExchange.Topic;
                        break;
                    case ExchangeType.Headers:
                        exchange.Name = DefaultExchange.Headers;
                        break;
                    default:
                        exchange.Name = DefaultExchange.Direct;
                        break;
                }
            }

            return settings;
        }
    }
}
