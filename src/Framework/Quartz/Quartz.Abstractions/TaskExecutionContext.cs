using System;
using Quartz;

namespace NetModular.Lib.Quartz.Abstractions
{
    public class TaskExecutionContext : ITaskExecutionContext
    {
        public Guid JobId { get; set; }

        public IJobExecutionContext JobExecutionContext { get; set; }
    }
}
