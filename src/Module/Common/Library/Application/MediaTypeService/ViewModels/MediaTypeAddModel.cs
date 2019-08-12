using System.ComponentModel.DataAnnotations;

namespace Tm.Module.Common.Application.MediaTypeService.ViewModels
{
    public class MediaTypeAddModel
    {
        [Required(ErrorMessage = "请输入扩展名")]
        public string Ext { get; set; }

        [Required(ErrorMessage = "请输入值")]
        public string Value { get; set; }
    }
}
