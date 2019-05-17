using System;
using System.ComponentModel.DataAnnotations;

namespace Nm.Module.CodeGenerator.Application.PropertyService.ViewModels
{
    public class PropertyUpdateNullableModel
    {
        [Required(ErrorMessage = "请选择属性")]
        public Guid Id { get; set; }

        /// <summary>
        /// 是否可空
        /// </summary>
        public bool Nullable { get; set; }
    }
}
