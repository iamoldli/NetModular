using System;
using Tm.Lib.Data.Abstractions.Attributes;
using Tm.Lib.Data.Core.Entities;

namespace Tm.Module.Admin.Domain.RoleMenuButton
{
    /// <summary>
    /// 角色菜单按钮
    /// </summary>
    [Table("Role_Menu_Button")]
    public class RoleMenuButtonEntity : Entity<int>
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 菜单编号
        /// </summary>
        public Guid MenuId { get; set; }

        /// <summary>
        /// 按钮编号
        /// </summary>
        public Guid ButtonId { get; set; }
    }
}
