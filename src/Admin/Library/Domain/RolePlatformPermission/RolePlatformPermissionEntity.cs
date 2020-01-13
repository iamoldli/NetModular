using System;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities;

namespace NetModular.Module.Admin.Domain.RolePlatformPermission
{
    /// <summary>
    /// 平台权限关联信息
    /// </summary>
    [Table("Role_Platform_Permission")]
    public class RolePlatformPermissionEntity : Entity<int>
    {
        /// <summary>
        /// 角色
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 平台类型
        /// </summary>
        public Platform Platform { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        public string PermissionCode { get; set; }
    }
}
