using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Nm.Module.CodeGenerator.Domain.ModelProperty;

namespace Nm.Module.CodeGenerator.Application.ModelPropertyService.ViewModels
{
    /// <summary>
    /// 从实体导入模型
    /// </summary>
    public class ModelPropertyImportModel
    {
        /// <summary>
        /// 所属实体
        /// </summary>
        [Required(ErrorMessage = "选择实体")]
        public Guid ClassId { get; set; }

        /// <summary>
        /// 模型类型
        /// </summary>
        [Required(ErrorMessage = "请选择模型类型")]
        public ModelType ModelType { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 实体属性ID列表
        /// </summary>
        public List<Guid> Ids { get; set; }
    }
}
