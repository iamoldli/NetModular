using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.MimeService.ViewModels
{
    public class MimeAddModel
    {
        [Required(ErrorMessage = "请输入扩展名")]
        public string Ext { get; set; }

        [Required(ErrorMessage = "请输入值")]
        public string Value { get; set; }
    }
}
