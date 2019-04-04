using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.ModuleInfoService.ViewModels
{
    /// <summary>
    /// 模块创建模型
    /// </summary>
    public class ModuleInfoAddModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "请输入模块名称")]
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required(ErrorMessage = "请输入模块编码")]
        public string Code { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; } = string.Empty;
    }
}
