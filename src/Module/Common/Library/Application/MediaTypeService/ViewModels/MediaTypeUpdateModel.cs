using System.ComponentModel.DataAnnotations;

namespace Nm.Module.Common.Application.MediaTypeService.ViewModels
{
    public class MediaTypeUpdateModel : MediaTypeAddModel
    {
        [Required(ErrorMessage = "请选择数据")]
        public int Id { get; set; }
    }
}
