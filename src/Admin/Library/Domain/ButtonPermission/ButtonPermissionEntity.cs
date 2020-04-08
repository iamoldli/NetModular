using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities;

namespace NetModular.Module.Admin.Domain.ButtonPermission
{
    /// <summary>
    /// 按钮权限
    /// </summary>
    [Table("Button_Permission")]
    [Tenant]
    public class ButtonPermissionEntity : Entity<int>
    {
        /// <summary>
        /// 按钮编码
        /// </summary>
        public string ButtonCode { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        [Length(200)]
        public string PermissionCode { get; set; }
    }
}
