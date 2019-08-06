using System.ComponentModel;

namespace Nm.Module.Quartz.Domain.Job
{
    /// <summary>
    /// 触发器类型
    /// </summary>
    public enum TriggerType
    {
        [Description("简单触发器")]
        Simple,
        [Description("CRON触发器")]
        Cron
    }
}
