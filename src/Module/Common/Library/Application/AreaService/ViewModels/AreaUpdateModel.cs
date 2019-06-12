using System.ComponentModel.DataAnnotations;

namespace Nm.Module.Common.Application.AreaService.ViewModels
{
    /// <summary>
    /// 区划代码添加模型
    /// </summary>
    public class AreaUpdateModel : AreaAddModel
    {
        [Required(ErrorMessage = "请选择要修改的区划代码")]
        public int Id { get; set; }
    }
}
