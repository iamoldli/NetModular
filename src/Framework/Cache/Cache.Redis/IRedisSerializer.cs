using StackExchange.Redis;

namespace NetModular.Lib.Cache.Redis
{
    /// <summary>
    /// Redis序列化器
    /// </summary>
    public interface IRedisSerializer
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        string Serialize<T>(T value);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        T Deserialize<T>(RedisValue value);
    }
}
