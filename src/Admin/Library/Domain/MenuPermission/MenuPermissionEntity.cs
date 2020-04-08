using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities;

namespace NetModular.Module.Admin.Domain.MenuPermission
{
    /// <summary>
    /// 菜单权限
    /// </summary>
    [Table("Menu_Permission")]
    [Tenant]
    public class MenuPermissionEntity : Entity<int>
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        public string MenuCode { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        [Length(200)]
        public string PermissionCode { get; set; }
    }
}
