using System;
using System.Threading;
using System.Threading.Tasks;
using Quartz;

namespace Nm.Module.Quartz.Infrastructure.Jobs
{
    public class TimerJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("下载运行：" + context.Trigger.GetNextFireTimeUtc().Value.ToLocalTime());
            await Console.Out.WriteLineAsync("Hello QuartzNet..." + DateTime.Now + Environment.NewLine);

            Thread.Sleep(5000);
        }
    }
}
