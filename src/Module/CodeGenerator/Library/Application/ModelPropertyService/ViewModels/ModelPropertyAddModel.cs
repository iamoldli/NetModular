using System;
using System.ComponentModel.DataAnnotations;

namespace Tm.Module.CodeGenerator.Application.ModelPropertyService.ViewModels
{
    public class ModelPropertyAddModel : ModelPropertyBaseModel
    {
        /// <summary>
        /// 所属实体
        /// </summary>
        [Required(ErrorMessage = "选择实体")]
        public Guid ClassId { get; set; }
    }
}
