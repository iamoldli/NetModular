using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.CodeGenerator.Application.EnumService.ViewModels
{
    /// <summary>
    /// 添加模型
    /// </summary>
    public class EnumAddModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "请输入名称")]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required(ErrorMessage = "请输入备注")]
        public string Remarks { get; set; }
    }
}
