using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Core.Entities.Extend;
using System;

namespace Nm.Module.Admin.Domain.Role
{
    /// <summary>
    /// 角色
    /// </summary>
    [Table("Role")]
    public partial class RoleEntity : EntityBaseWithSoftDelete
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 归属公司
        /// </summary>
        public Guid CID { get; set; }
    }
}
