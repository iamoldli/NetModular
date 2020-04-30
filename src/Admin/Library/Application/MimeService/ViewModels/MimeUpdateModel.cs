using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.MimeService.ViewModels
{
    public class MimeUpdateModel : MimeAddModel
    {
        [Required(ErrorMessage = "请选择数据")]
        public int Id { get; set; }
    }
}
