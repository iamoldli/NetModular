using Tm.Lib.Data.Abstractions.Attributes;
using Tm.Lib.Data.Core.Entities.Extend;
using Tm.Lib.Utils.Core.Enums;

namespace Tm.Module.Admin.Domain.Permission
{
    /// <summary>
    /// 权限
    /// </summary>
    [Table("Permission")]
    public partial class PermissionEntity : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属模块编码(与请求中的Area对应)
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 功能
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        public HttpMethod HttpMethod { get; set; }

        /// <summary>
        /// 唯一编码
        /// </summary>
        public string Code { get; set; }
    }
}
