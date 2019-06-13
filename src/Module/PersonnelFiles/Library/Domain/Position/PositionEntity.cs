using System;
using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Core.Entities.Extend;

namespace Nm.Module.PersonnelFiles.Domain.Position
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

    }
}
