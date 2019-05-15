using System;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities;

namespace NetModular.Module.Admin.Domain.MenuPermission
{
    /// <summary>
    /// 菜单权限
    /// </summary>
    [Table("Menu_Permission")]
    public class MenuPermissionEntity : Entity<int>
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        public Guid MenuId { get; set; }

        /// <summary>
        /// 权限编号
        /// </summary>
        public Guid PermissionId { get; set; }
    }
}
