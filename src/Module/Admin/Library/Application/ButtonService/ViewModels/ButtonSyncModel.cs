using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NetModular.Module.Admin.Domain.Button;

namespace NetModular.Module.Admin.Application.ButtonService.ViewModels
{
    public class ButtonSyncModel
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        [Required(ErrorMessage = "请选择菜单")]
        public Guid MenuId { get; set; }

        /// <summary>
        /// 按钮列表
        /// </summary>
        public List<ButtonEntity> Buttons { get; set; }
    }
}
