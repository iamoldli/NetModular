using System;
using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.ModuleInfoService.ViewModels
{
    /// <summary>
    /// 模块编辑模型
    /// </summary>
    public class ModuleInfoUpdateModel : ModuleInfoAddModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Required(ErrorMessage = "请选择模块")]
        public Guid Id { get; set; }
    }
}
