using System.ComponentModel;

namespace Nm.Module.Quartz.Domain.Job
{
    /// <summary>
    /// 任务状态
    /// </summary>
    public enum JobStatus
    {
        [Description("未知")]
        UnKnown,
        [Description("停止")]
        Stop,
        [Description("启动")]
        Start,
        [Description("异常")]
        Error
    }
}
