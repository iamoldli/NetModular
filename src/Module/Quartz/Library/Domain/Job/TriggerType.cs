using System.ComponentModel;

namespace Nm.Module.Quartz.Domain.Job
{
    /// <summary>
    /// 触发器类型
    /// </summary>
    public enum TriggerType
    {
        [Description("未知")]
        UnKnown,
        [Description("简单")]
        Simple,
        [Description("Cron表达式")]
        Cron
    }
}
