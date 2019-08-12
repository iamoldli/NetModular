using System;
using Tm.Lib.Quartz.Abstractions;
using Quartz;

namespace Tm.Lib.Quartz.Core
{
    public class JobTaskContext : IJobTaskContext
    {
        public Guid JobId { get; set; }
        public IJobExecutionContext JobExecutionContext { get; set; }
    }
}
