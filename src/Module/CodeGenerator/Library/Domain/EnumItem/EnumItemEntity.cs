using System;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities;

namespace NetModular.Module.CodeGenerator.Domain.EnumItem
{
    /// <summary>
    /// 枚举项实体
    /// </summary>
    [Table("Enum_Item")]
    public class EnumItemEntity : Entity
    {
        /// <summary>
        /// 所属枚举编号
        /// </summary>
        public Guid EnumId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}
