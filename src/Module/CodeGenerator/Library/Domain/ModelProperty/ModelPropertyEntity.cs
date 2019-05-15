using System;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities.Extend;
using NetModular.Module.CodeGenerator.Domain.Property;

namespace NetModular.Module.CodeGenerator.Domain.ModelProperty
{
    [Table("Model_Property")]
    public partial class ModelPropertyEntity : EntityBase
    {
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
