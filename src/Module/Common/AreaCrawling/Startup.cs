using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nm.Module.Common.Application.AreaService;

namespace Nm.Module.Common.AreaCrawling
{
    public class Startup : IHostedService, IDisposable
    {
        private readonly IAreaService _service;
        private readonly ILogger _logger;

        public Startup(IAreaService service, ILogger<Startup> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("开始爬取区域代码数据");

                await _service.Crawling();

                _logger.LogInformation("爬取结束");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "数据同步失败");
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
           
        }

        public void Dispose()
        {

        }
    }
}
