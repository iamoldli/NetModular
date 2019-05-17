using System;
using System.ComponentModel.DataAnnotations;

namespace Nm.Module.CodeGenerator.Application.EnumService.ViewModels
{
    public class EnumUpdateModel : EnumAddModel
    {
        [Required(ErrorMessage = "请选择要修改的枚举")]
        public Guid Id { get; set; }
    }
}
