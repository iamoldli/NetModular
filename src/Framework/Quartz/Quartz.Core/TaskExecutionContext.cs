using System;
using NetModular.Lib.Quartz.Abstractions;
using Quartz;

namespace NetModular.Lib.Quartz.Core
{
    public class TaskExecutionContext : ITaskExecutionContext
    {
        public Guid JobId { get; set; }

        public IJobExecutionContext JobExecutionContext { get; set; }
    }
}
