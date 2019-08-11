using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nm.Lib.MQ.RabbitMQ;
using Xunit;

namespace MQ.RabbitMQ.Test
{
    public class RabbitMQClientTest
    {
        private readonly RabbitMQClient _client;
        public RabbitMQClientTest()
        {
            var service = new ServiceCollection();
            service.AddRabbitMQ("Production");

            _client = service.BuildServiceProvider().GetService<RabbitMQClient>();

            //_client.Receive<Product>("product", "product", p =>
            //{
            //    Debug.WriteLine(p.Id + p.Title);
            //    return true;
            //});

            //Thread.Sleep(100000);
        }

        [Fact]
        public void SendTest()
        {
            var list = new List<Product>();
            for (int i = 0; i < 10000; i++)
            {
                list.Add(new Product
                {
                    Id = i,
                    Title = "三星Note 10",
                    Price = 6999,
                    Store = 1000
                });
            }

            var sw = new Stopwatch();
            sw.Start();

            foreach (var product in list)
            {
                _client.Send("product", "product", product);
            }

            sw.Stop();
            var s = sw.ElapsedMilliseconds;
        }
    }
}
