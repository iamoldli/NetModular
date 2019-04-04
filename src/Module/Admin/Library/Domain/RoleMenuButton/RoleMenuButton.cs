using System;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities;

namespace NetModular.Module.Admin.Domain.RoleMenuButton
{
    /// <summary>
    /// 角色菜单按钮
    /// </summary>
    [Table("Role_Menu_Button")]
    public class RoleMenuButton : Entity<int>
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
