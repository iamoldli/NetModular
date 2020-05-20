using System.Diagnostics;
using System.Threading;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Cache.Redis;
using Xunit;

namespace Cache.Redis.Tests
{
    public class RedisHelperTest
    {
        private readonly RedisHelper _helper = new RedisHelper(new CacheConfig
        {
            Provider = CacheProvider.Redis,
            Redis = new RedisConfig
            {
                ConnectionString = "127.0.0.1",
                Prefix = "UnitTest"
            }
        });

        [Fact]
        public async void SubscribeTest()
        {
            var subscriber = _helper.Conn.GetSubscriber();
            await subscriber.SubscribeAsync("log", (channel, msg) =>
            {
                Trace.WriteLine(msg);
            });

            var pub = _helper.Conn.GetSubscriber();
            var i = 100;
            while (i > 0)
            {
                await pub.PublishAsync("log", i);
                i--;
                Thread.Sleep(500);
            }

            Assert.NotNull(subscriber);
        }
    }
}
