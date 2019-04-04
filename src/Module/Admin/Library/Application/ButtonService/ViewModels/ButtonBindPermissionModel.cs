using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.ButtonService.ViewModels
{
    public class ButtonBindPermissionModel
    {
        /// <summary>
        /// 菜单标识
        /// </summary>
        [Required(ErrorMessage = "请指定按钮")]
        public Guid Id { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<Guid> PermissionList { get; set; }
    }
}
