using System;
using System.ComponentModel.DataAnnotations;

namespace Tm.Module.CodeGenerator.Application.PropertyService.ViewModels
{
    public class PropertyUpdateModel : PropertyBaseModel
    {
        [Required(ErrorMessage = "请选择要编辑的属性")]
        public Guid Id { get; set; }
    }
}
