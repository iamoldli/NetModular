using System;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities;

namespace NetModular.Module.Admin.Domain.RoleButton
{
    /// <summary>
    /// 角色按钮绑定关系
    /// </summary>
    [Table("Role_Button")]
    public class RoleButtonEntity : Entity<int>
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 页面编码
        /// </summary>
        [Length(200)]
        public string PageCode { get; set; }

        /// <summary>
        /// 按钮编码
        /// </summary>
        [Length(200)]
        public string ButtonCode { get; set; }
    }
}
