using System;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Query;

namespace NetModular.Module.Admin.Domain.AuditInfo.Models
{
    public class AuditInfoQueryModel : QueryModel
    {
        /// <summary>
        /// 访问来源
        /// </summary>
        public Platform? Platform { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        public override int ExportCountLimit => 100;
    }
}
