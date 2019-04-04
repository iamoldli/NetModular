using System;
using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.ButtonService.ViewModels
{
    /// <summary>
    /// 按钮添加模型
    /// </summary>
    public class ButtonAddModel
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        [Required(ErrorMessage = "请选择菜单")]
        public Guid MenuId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "请输入名称")]
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required(ErrorMessage = "请输入按钮编码")]
        public string Code { get; set; }
    }
}
