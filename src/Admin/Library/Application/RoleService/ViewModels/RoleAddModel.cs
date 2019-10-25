using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.RoleService.ViewModels
{
    public class RoleAddModel
    {
        [Required(ErrorMessage = "请输入角色名称")]
        public string Name { get; set; }

        public string Remarks { get; set; }

        public int Sort { get; set; }
    }
}
