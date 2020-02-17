using System;
using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.RoleService.ViewModels
{
    /// <summary>
    /// 角色菜单按钮绑定模型
    /// </summary>
    public class RoleMenuButtonBindModel
    {
        [Required(ErrorMessage = "请选择角色")]
        public Guid RoleId { get; set; }

        [Required(ErrorMessage = "请选择菜单")]
        public Guid MenuId { get; set; }

        [Required(ErrorMessage = "请选择按钮")]
        public Guid ButtonId { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked { get; set; }
    }
}
