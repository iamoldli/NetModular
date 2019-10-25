using System;
using System.Threading.Tasks;
using NetModular.Lib.Quartz.Abstractions;
using Quartz;

namespace NetModular.Lib.Quartz.Core
{
    public abstract class JobTaskAbstract : IJobTask
    {
        protected readonly IJobLogger Logger;

        protected JobTaskAbstract(IJobLogger logger)
        {
            Logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var jobId = context.JobDetail.JobDataMap["id"];
            Logger.JobId = jobId == null ? Guid.Empty : Guid.Parse(jobId.ToString());

            await Logger.Info("任务开始");

            try
            {
                await Execute(new JobTaskContext
                {
                    JobId = Logger.JobId,
                    JobExecutionContext = context
                });
            }
            catch (Exception ex)
            {
                await Logger.Error("任务异常：" + ex);
            }

            await Logger.Info("任务结束");
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract Task Execute(IJobTaskContext context);
    }
}
