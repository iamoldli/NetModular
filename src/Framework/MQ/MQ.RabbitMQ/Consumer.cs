using NetModular.Lib.Utils.Core.Extensions;
using RabbitMQ.Client;

namespace NetModular.Lib.MQ.RabbitMQ
{
    /// <summary>
    /// 消费者
    /// </summary>
    public class Consumer
    {
        /// <summary>
        /// 消费者标签
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 关联通道
        /// </summary>
        public IModel Channel { get; set; }

        /// <summary>
        /// 取消消费
        /// </summary>
        public void Cancel()
        {
            if (Tag.NotNull())
            {
                Channel.BasicCancel(Tag);
            }
        }
    }
}
