using System;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Core.Entities.Extend;

namespace Nm.Module.Admin.Domain.Account
{
    /// <summary>
    /// 账户
    /// </summary>
    [Table("Account")]
    public partial class AccountEntity : EntityBaseWithSoftDelete
    {
        /// <summary>
        /// 类型
        /// </summary>
        public AccountType Type { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LoginTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LoginIP { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public AccountStatus Status { get; set; }

        /// <summary>
        /// 是否锁定，锁定后不允许在账户管理中修改
        /// </summary>
        public bool IsLock { get; set; }

        /// <summary>
        /// 注销时间
        /// </summary>
        public DateTime ClosedTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 注销人
        /// </summary>
        public Guid ClosedBy { get; set; } = Guid.Empty;
    }
}
