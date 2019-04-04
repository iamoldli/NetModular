using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.MenuService.ViewModels
{
    public class MenuBindPermissionModel
    {
        /// <summary>
        /// 菜单标识
        /// </summary>
        [Required(ErrorMessage = "请指定菜单")]
        public Guid Id { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<Guid> PermissionList { get; set; }
    }
}
