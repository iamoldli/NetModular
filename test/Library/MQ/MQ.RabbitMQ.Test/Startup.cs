using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nm.Lib.MQ.RabbitMQ;

namespace MQ.RabbitMQ.Test
{
    public class Startup : IHostedService, IDisposable
    {
        private readonly RabbitMQClient _client;
        private readonly ILogger _logger;

        public Startup(ILogger<Startup> logger, RabbitMQClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Test2();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
        }

        public void Dispose()
        {
        }

        /// <summary>
        /// 公平分发测试
        /// </summary>
        public void Test1()
        {
            _client.Receive<Product>("product", p =>
            {
                _logger.LogInformation("消费者1：" + p.Id);
                return true;
            });

            _client.Receive<Product>("product", p =>
           {
               _logger.LogInformation("消费者2：" + p.Id);
               return true;
           });

            for (int i = 0; i < 1000; i++)
            {
                var product = new Product
                {
                    Id = i,
                    Title = "三星Note 10",
                    Price = 6999,
                    Store = 1000
                };

                _client.Send("product", "product", product);
            }
        }

        public void Test2()
        {
            //_client.Receive<Product>("product", p =>
            //{
            //    _logger.LogInformation("消费者1：" + p.Id);
            //    return true;
            //});

            var product = new Product
            {
                Id = 1,
                Title = "三星Note 10",
                Price = 6999,
                Store = 1000
            };

            _client.Send("product", "product", product);

            product.Id = 2;
            _client.Send("product1", "product", product);

        }
    }
}