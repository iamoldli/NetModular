using System;
using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Core.Entities.Extend;

namespace Nm.Module.Quartz.Domain.Job
{
    /// <summary>
    /// 任务
    /// </summary>
    [Table("Job")]
    public partial class JobEntity : EntityBase
    {
        /// <summary>
        /// 任务组编号
        /// </summary>
        public Guid Group { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 触发器类型
        /// </summary>
        public TriggerType TriggerType { get; set; }

        /// <summary>
        /// 组件编号
        /// </summary>
        public Guid Component { get; set; }

        /// <summary>
        /// 类编号
        /// </summary>
        public Guid Class { get; set; }

        /// <summary>
        /// 简单触发器时间间隔
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// 简单触发器重复次数，0表示无限
        /// </summary>
        public int RepeatCount { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string Cron { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public JobStatus Status { get; set; }

    }
}
