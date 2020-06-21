using System;
using System.ComponentModel;

namespace NetModular.Lib.Auth.Abstractions.LoginModels
{
    public class LoginResultModel
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// 租户编号
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 账户编号
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// 账户类型
        /// </summary>
        public AccountType AccountType { get; set; }

        /// <summary>
        /// 登录平台
        /// </summary>
        public Platform Platform { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 登录模式
        /// </summary>
        public LoginMode LoginMode { get; set; }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string RefreshToken { get; set; }
    }

    /// <summary>
    /// 登录模式
    /// </summary>
    public enum LoginMode
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Description("用户名")]
        UserName,
        /// <summary>
        /// 邮箱
        /// </summary>
        [Description("邮箱")]
        Email,
        /// <summary>
        /// 用户名或邮箱
        /// </summary>
        [Description("用户名或邮箱")]
        UserNameOrEmail,
        /// <summary>
        /// 手机号
        /// </summary>
        [Description("手机号")]
        Phone,
        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        WeChat,
        /// <summary>
        /// QQ
        /// </summary>
        [Description("QQ")]
        QQ,
        /// <summary>
        /// GitHub
        /// </summary>
        [Description("GitHub")]
        GitHub,
        /// <summary>
        /// 自定义
        /// </summary>
        [Description("自定义")]
        Custom,
    }
}
