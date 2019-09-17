namespace MQ.RabbitMQ.Test
{
public interface Product
{
    private readonly Logger _logger = new Logger();
    public void Create()
    {
        //创建商品

        _logger.Debug("创建产品");
    }
}
}
