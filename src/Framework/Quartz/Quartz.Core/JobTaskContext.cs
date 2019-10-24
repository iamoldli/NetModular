using System;
using Nm.Lib.Quartz.Abstractions;
using Quartz;

namespace Nm.Lib.Quartz.Core
{
    public class JobTaskContext : IJobTaskContext
    {
        public Guid JobId { get; set; }
        public IJobExecutionContext JobExecutionContext { get; set; }
    }
}
