using System;
using System.ComponentModel;
using System.Threading.Tasks;
using NetModular.Lib.Quartz.Abstractions;
using NetModular.Lib.Quartz.Core;

namespace NetModular.Module.Admin.Quartz
{
    public class TimerJob : JobTaskAbstract
    {
        [Description("定时输出时间")]
        public TimerJob(IJobLogger logger) : base(logger)
        {
        }

        public override async Task Execute(IJobTaskContext context)
        {
            await Logger.Info(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
