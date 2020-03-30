using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Cache.Redis;
using Newtonsoft.Json;
using Xunit;

namespace Cache.Redis.Tests
{
    public class RedisHelperTest
    {
        private RedisHelper _helper = new RedisHelper(new RedisOptions { Prefix = "Test:", ConnectionString = "127.0.0.1" });

        [Fact]
        public void StringSetAsyncTest()
        {
            var b = _helper.StringSetAsync("StringSetAsync", Guid.NewGuid()).Result;
            Assert.True(b);
        }

        [Fact]
        public void SortedSetAddAsyncTest()
        {
            var key = "test_sortedset";
            var ran = new Random();
            for (int i = 0; i < 10000; i++)
            {
                _helper.SortedSetAddAsync(key, Guid.NewGuid(), ran.Next(1, 10000)).GetAwaiter();
            }

            var count = _helper.SortedSetLengthAsync(key).Result;
            Assert.Equal(10000, count);
        }
    }
}
