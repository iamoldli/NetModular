using System;
using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.ButtonService.ViewModels
{
    /// <summary>
    /// 修改按钮
    /// </summary>
    public class ButtonUpdateModel
    {
        [Required(ErrorMessage = "请选择要修改的按钮")]
        public Guid Id { get; set; }

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

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
