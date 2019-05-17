using System;
using System.ComponentModel.DataAnnotations;
using Nm.Lib.Data.Query;

namespace Nm.Module.CodeGenerator.Domain.ModelProperty.Models
{
    public class ModelPropertyQueryModel : QueryModel
    {
        [Required(ErrorMessage = "请选择实体")]
        public Guid ClassId { get; set; }

        /// <summary>
        /// 模型类型
        /// </summary>
        public ModelType ModelType { get; set; }
    }
}
