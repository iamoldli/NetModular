using System;
using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Core.Entities;

namespace Nm.Module.Quartz.Domain.JobClass
{
    /// <summary>
    /// 任务类型
    /// </summary>
    [Table("Job_Class")]
    public partial class JobClassEntity : Entity<int>
    {
        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Desc { get; set; }

    }
}
