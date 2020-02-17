using System;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities;

namespace NetModular.Module.Admin.Domain.AccountAuthInfo
{
    /// <summary>
    /// 账户认证信息
    /// </summary>
    [Table("Account_Auth_Info")]
    public class AccountAuthInfoEntity : Entity<int>
    {
        /// <summary>
        /// 账户编号
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// 登录平台
        /// </summary>
        public Platform Platform { get; set; }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 刷新令牌过期时间
        /// </summary>
        public DateTime RefreshTokenExpiredTime { get; set; }

        /// <summary>
        /// 最后登录时间戳
        /// </summary>
        public long LoginTime { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        [Nullable]
        public string LoginIP { get; set; } = string.Empty;
    }
}
