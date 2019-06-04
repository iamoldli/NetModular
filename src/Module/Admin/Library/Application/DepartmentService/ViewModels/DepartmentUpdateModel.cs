using System;
using System.ComponentModel.DataAnnotations;
using Nm.Module.Admin.Application.RoleService.ViewModels;

namespace Nm.Module.Admin.Application.DepartmentService.ViewModels
{
    public class DepartmentUpdateModel : RoleAddModel
    {
        [Required(ErrorMessage = "请选择角色")]
        public Guid Id { get; set; }
    }
}
