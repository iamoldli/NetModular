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
        /// 所属模块
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 任务唯一键
        /// </summary>
        [Length(100)]
        public string JobKey { get; set; }

        /// <summary>
        /// 任务组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        [Length(100)]
        public string Name { get; set; }

        /// <summary>
        /// 任务编码
        /// </summary>
        [Length(100)]
        public string Code { get; set; }

        /// <summary>
        /// 任务类
        /// </summary>
        [Length(300)]
        public string JobClass { get; set; }

        /// <summary>
        /// 触发类型
        /// </summary>
        public TriggerType TriggerType { get; set; }

        /// <summary>
        /// 简单触发器时间间隔(秒)
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
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public JobStatus Status { get; set; }
    }
}
