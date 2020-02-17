using System;
using Quartz;

namespace NetModular.Lib.Quartz.Abstractions
{
    /// <summary>
    /// 任务执行上下文
    /// </summary>
    public interface ITaskExecutionContext
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        Guid JobId { get; set; }

        /// <summary>
        /// Quartz的任务执行上下文
        /// </summary>
        IJobExecutionContext JobExecutionContext { get; set; }
    }
}
