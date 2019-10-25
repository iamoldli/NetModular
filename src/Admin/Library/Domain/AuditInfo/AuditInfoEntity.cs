using System;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities;

namespace NetModular.Module.Admin.Domain.AuditInfo
{
    /// <summary>
    /// 审计信息
    /// </summary>
    [Table("AuditInfo")]
    public partial class AuditInfoEntity : Entity<long>
    {
        /// <summary>
        /// 账户编号
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 控制器描述
        /// </summary>
        [Nullable]
        public string ControllerDesc { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        [Nullable]
        public string ActionDesc { get; set; }

        /// <summary>
        /// 参数(Json序列化)
        /// </summary>
        [Max]
        [Nullable]
        public string Parameters { get; set; }

        /// <summary>
        /// 返回值(Json序列化)
        /// </summary>
        [Max]
        [Nullable]
        public string Result { get; set; }

        /// <summary>
        /// 方法开始执行时间
        /// </summary>
        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// 方法执行总用时(ms)
        /// </summary>
        public long ExecutionDuration { get; set; }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        [Length(500)]
        public string BrowserInfo { get; set; }

        /// <summary>
        /// 平台
        /// </summary>
        public Platform Platform { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }
    }
}
