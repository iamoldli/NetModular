using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Tm.Lib.Quartz.Abstractions;
using Tm.Lib.Quartz.Core;

namespace Tm.Module.Quartz.Quartz
{
    [Description("定时任务")]
    public class TimerJob : JobTaskAbstract
    {
        public TimerJob(IJobLogger logger) : base(logger)
        {
        }

        public override Task Execute(IJobTaskContext context)
        {
            Logger.Info($"当前任务编号：{context.JobId},执行时间：{DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
