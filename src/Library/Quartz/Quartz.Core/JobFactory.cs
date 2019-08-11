using System;
using Quartz;
using Quartz.Spi;

namespace Nm.Lib.Quartz.Core
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider _container;

        public JobFactory(IServiceProvider container)
        {
            _container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _container.GetService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            (job as IDisposable)?.Dispose();
        }
    }
}
