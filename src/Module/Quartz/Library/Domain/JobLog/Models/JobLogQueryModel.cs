using System;
using System.ComponentModel.DataAnnotations;
using Nm.Lib.Data.Query;

namespace Nm.Module.Quartz.Domain.JobLog.Models
{
    public class JobLogQueryModel : QueryModel
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        [Required(ErrorMessage = "请选择任务")]
        public Guid JobId { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
