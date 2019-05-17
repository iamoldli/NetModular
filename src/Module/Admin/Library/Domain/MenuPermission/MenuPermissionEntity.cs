using System;
using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Core.Entities;

namespace Nm.Module.Admin.Domain.MenuPermission
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
