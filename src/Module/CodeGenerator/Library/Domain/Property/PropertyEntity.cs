using System;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities.Extend;

namespace NetModular.Module.CodeGenerator.Domain.Property
{
    /// <summary>
    /// 属性信息
    /// </summary>
    [Table("Property")]
    public partial class PropertyEntity : EntityBase
    {
        /// <summary>
        /// 类编号
        /// </summary>
        public Guid ClassId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 是否继承自基类实体
        /// </summary>
        public bool IsInherit { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public PropertyType Type { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        public int Precision { get; set; }

        /// <summary>
        /// 刻度
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// 关联枚举
        /// </summary>
        public Guid EnumId { get; set; }

        /// <summary>
        /// 可空
        /// </summary>
        public bool Nullable { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否在列表页中显示
        /// </summary>
        public bool ShowInList { get; set; }
    }
}
