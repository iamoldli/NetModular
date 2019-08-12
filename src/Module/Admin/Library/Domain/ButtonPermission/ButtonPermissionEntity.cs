using Tm.Lib.Data.Abstractions.Attributes;
using Tm.Lib.Data.Core.Entities;

namespace Tm.Module.Admin.Domain.ButtonPermission
{
    /// <summary>
    /// 按钮权限
    /// </summary>
    [Table("Button_Permission")]
    public class ButtonPermissionEntity : Entity<int>
    {
        /// <summary>
        /// 按钮编码
        /// </summary>
        public string ButtonCode { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        public string PermissionCode { get; set; }
    }
}
