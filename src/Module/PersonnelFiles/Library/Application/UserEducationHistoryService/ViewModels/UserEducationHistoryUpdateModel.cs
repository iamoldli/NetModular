using System.ComponentModel.DataAnnotations;

namespace Nm.Module.PersonnelFiles.Application.UserEducationHistoryService.ViewModels
{
    public class UserEducationHistoryUpdateModel : UserEducationHistoryAddModel
    {
        [Required(ErrorMessage = "请选择数据")]
        public int Id { get; set; }
    }
}
