using System;
using Tm.Lib.Data.Abstractions.Attributes;
using Tm.Lib.Data.Core.Entities.Extend;

namespace Tm.Module.PersonnelFiles.Domain.Position
{
    /// <summary>
    /// 岗位
    /// </summary>
    [Table("Position")]
    public partial class PositionEntity : EntityBase
    {
        /// <summary>
        /// 部门编码
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }
}
