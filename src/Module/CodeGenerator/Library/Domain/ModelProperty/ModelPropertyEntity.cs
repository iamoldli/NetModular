using System;
using Tm.Lib.Data.Abstractions.Attributes;
using Tm.Lib.Data.Core.Entities.Extend;
using Tm.Module.CodeGenerator.Domain.Property;

namespace Tm.Module.CodeGenerator.Domain.ModelProperty
{
    [Table("Model_Property")]
    public partial class ModelPropertyEntity : EntityBase
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// 所属类
        /// </summary>
        public Guid ClassId { get; set; }

        /// <summary>
        /// 模型类型
        /// </summary>
        public ModelType ModelType { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public PropertyType Type { get; set; }

        /// <summary>
        /// 可空
        /// </summary>
        public bool Nullable { get; set; }

        /// <summary>
        /// 关联枚举
        /// </summary>
        public Guid EnumId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }
    }
}
