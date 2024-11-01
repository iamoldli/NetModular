using System.Threading.Tasks;
using RabbitMQ.Client;

namespace NetModular.Lib.MQ.RabbitMQ;

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
    public IChannel Channel { get; set; }

    /// <summary>
    /// 取消消费
    /// </summary>
    public Task CancelAsync()
    {
        if (Tag.NotNull())
        {
            return Channel.BasicCancelAsync(Tag);
        }

        return Task.CompletedTask;
    }
}