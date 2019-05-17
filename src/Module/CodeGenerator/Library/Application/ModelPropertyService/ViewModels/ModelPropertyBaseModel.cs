using System;
using System.ComponentModel.DataAnnotations;
using Nm.Module.CodeGenerator.Domain.ModelProperty;
using Nm.Module.CodeGenerator.Domain.Property;

namespace Nm.Module.CodeGenerator.Application.ModelPropertyService.ViewModels
{
    public class ModelPropertyBaseModel
    {
        /// <summary>
        /// 模型类型
        /// </summary>
        [Required(ErrorMessage = "请选择模型类型")]
        public ModelType ModelType { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "请输入名称")]
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required(ErrorMessage = "请选择模型类型")]
        public PropertyType Type { get; set; }

        /// <summary>
        /// 可空
        /// </summary>
        public bool Nullable { get; set; }

        /// <summary>
        /// 关联枚举
        /// </summary>
        public Guid? EnumId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required(ErrorMessage = "请输入备注")]
        public string Remarks { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }
    }
}
