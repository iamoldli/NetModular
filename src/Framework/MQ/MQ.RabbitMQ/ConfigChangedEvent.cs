using NetModular.Lib.Config.Abstractions;

namespace NetModular.Lib.MQ.RabbitMQ
{
    public class ConfigChangedEvent : IConfigChangeEvent<RabbitMQConfig>
    {
        private readonly RabbitMQClient _client;

        public ConfigChangedEvent(RabbitMQClient client)
        {
            _client = client;
        }

        public void OnChanged(RabbitMQConfig newConfig, RabbitMQConfig oldConfig)
        {
            _client.CreateConnection();
        }
    }
}
