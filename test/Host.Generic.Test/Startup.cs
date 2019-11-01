using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Host.Generic.Test
{
    public class Startup : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private bool _run = true;

        public Startup(ILogger<Startup> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            while (_run)
            {
                _logger.LogInformation(DateTime.Now.ToString(CultureInfo.InvariantCulture));

                Thread.Sleep(1000);
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _run = false;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
