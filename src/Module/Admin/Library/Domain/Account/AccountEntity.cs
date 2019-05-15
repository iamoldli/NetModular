using System;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities.Extend;

namespace NetModular.Module.Admin.Domain.Account
{
    /// <summary>
    /// 账户
    /// </summary>
    [Table("Account")]
    public class AccountEntity : EntityBaseWithSoftDelete
    {
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
        public string Phone { get; set; } = String.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = String.Empty;

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LoginTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LoginIP { get; set; } = String.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public AccountStatus Status { get; set; }

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
