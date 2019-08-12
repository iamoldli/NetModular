using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Tm.Lib.Quartz.Abstractions;

namespace Tm.Lib.Quartz.Core
{
    public class JobLogger : IJobLogger
    {
        private readonly ILogger _logger;

        public JobLogger(ILogger<JobLogger> logger)
        {
            _logger = logger;
        }

        public Guid JobId { get; set; }

        public Task Info(string msg)
        {
            _logger.LogInformation($"任务编号:{JobId}, {msg}");
            return Task.CompletedTask;
        }

        public Task Debug(string msg)
        {
            _logger.LogDebug($"任务编号:{JobId}, {msg}");
            return Task.CompletedTask;
        }

        public Task Error(string msg)
        {
            _logger.LogError($"任务编号:{JobId}, {msg}");
            return Task.CompletedTask;
        }
    }
}
