using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.RoleService.ViewModels
{
    public class RoleMenuBindModel
    {
        [Required(ErrorMessage = "请选择角色")]
        public Guid RoleId { get; set; }

        /// <summary>
        /// 菜单列表
        /// </summary>
        public List<Guid> Menus { get; set; }
    }
}
