using System;
using NetModular.Lib.Data.Query;

namespace NetModular.Module.Admin.Application.AuditInfoService.ViewModels
{
    public class AuditInfoQueryModel : QueryModel
    {
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
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
