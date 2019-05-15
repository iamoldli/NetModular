using System;
using System.ComponentModel.DataAnnotations;
using NetModular.Lib.Data.Query;

namespace NetModular.Module.CodeGenerator.Domain.ModelProperty.Models
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
