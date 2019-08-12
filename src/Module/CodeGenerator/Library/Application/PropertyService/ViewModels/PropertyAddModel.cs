using System;
using System.ComponentModel.DataAnnotations;

namespace Tm.Module.CodeGenerator.Application.PropertyService.ViewModels
{
    public class PropertyAddModel : PropertyBaseModel
    {
        /// <summary>
        /// 类编号
        /// </summary>
        [Required(ErrorMessage = "请选择类")]
        public Guid ClassId { get; set; }
    }
}
