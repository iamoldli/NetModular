using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities.Extend;
using System;

namespace NetModular.Module.Admin.Domain.Role
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
        [Length(300)]
        public string Remarks { get; set; }

        /// <summary>
        /// 是否是指定的角色，如果是其它模块指定的，不允许删除修改
        /// </summary>
        public bool IsSpecified { get; set; }
    }
}
