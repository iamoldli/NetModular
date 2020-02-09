using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Quartz.Abstractions;

namespace NetModular.Lib.Quartz.Core
{
    public class TaskLogger : ITaskLogger
    {
        private readonly ILogger _logger;

        public TaskLogger(ILogger<TaskLogger> logger)
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
