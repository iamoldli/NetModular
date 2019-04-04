using System;
using NetModular.Lib.Utils.Core.Enums;
using NetModular.Lib.Utils.Core.Extensions;

namespace NetModular.Module.Admin.Application.AuditInfoService.ResultModels
{
    public class AuditInfoQueryResultModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 账户编号
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 方法开始执行时间
        /// </summary>
        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// 方法执行总用时(ms)
        /// </summary>
        public long ExecutionDuration { get; set; }

        /// <summary>
        /// 平台
        /// </summary>
        public Platform Platform { get; set; }

        /// <summary>
        /// 平台名称
        /// </summary>
        public string PlatformName => Platform.ToDescription();

        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }
    }
}
