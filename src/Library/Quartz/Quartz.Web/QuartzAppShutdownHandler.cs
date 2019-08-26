using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nm.Lib.Quartz.Abstractions;
using Nm.Lib.Utils.Core.Interfaces;

namespace Nm.Lib.Quartz.Web
{
    public class QuartzAppShutdownHandler : IAppShutdownHandler
    {
        private readonly IQuartzServer _server;
        private readonly ILogger _logger;

        public QuartzAppShutdownHandler(IQuartzServer server, ILogger<QuartzAppShutdownHandler> logger)
        {
            _server = server;
            _logger = logger;
        }

        public async Task Handle()
        {
            if (_server != null)
            {
                await _server.Stop();

                _logger.LogDebug("quartz server stop");
            }
        }
    }
}
