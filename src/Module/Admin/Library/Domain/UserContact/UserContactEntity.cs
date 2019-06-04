using System;
using Nm.Lib.Data.Core.Entities;

namespace Nm.Module.Admin.Domain.UserContact
{
    /// <summary>
    /// 练习方式
    /// </summary>
    public class UserContactEntity : Entity<int>
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 微信
        /// </summary>
        public string WeChat { get; set; }

        /// <summary>
        /// 钉钉
        /// </summary>
        public string DingDing { get; set; }
    }
}
