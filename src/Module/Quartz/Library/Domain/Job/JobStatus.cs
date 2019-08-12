using System.ComponentModel;

namespace Tm.Module.Quartz.Domain.Job
{
    /// <summary>
    /// 任务状态
    /// </summary>
    public enum JobStatus
    {
        [Description("运行")]
        Running,
        [Description("暂停")]
        Pause,
        [Description("已完成")]
        Completed,
        [Description("异常")]
        Exception
    }
}
