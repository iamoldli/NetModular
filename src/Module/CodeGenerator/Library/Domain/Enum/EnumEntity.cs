using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Core.Entities.Extend;

namespace Nm.Module.CodeGenerator.Domain.Enum
{
    /// <summary>
    /// 枚举实体
    /// </summary>
    [Table("Enum")]
    public class EnumEntity : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}
