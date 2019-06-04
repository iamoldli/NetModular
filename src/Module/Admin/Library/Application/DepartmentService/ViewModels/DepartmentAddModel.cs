using System.ComponentModel.DataAnnotations;

namespace Nm.Module.Admin.Application.DepartmentService.ViewModels
{
    public class DepartmentAddModel
    {
        [Required(ErrorMessage = "请输入角色名称")]
        public string Name { get; set; }

        public string Remarks { get; set; }

        public int Sort { get; set; }
    }
}
