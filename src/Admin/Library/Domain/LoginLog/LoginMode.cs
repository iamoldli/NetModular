using System.ComponentModel;

namespace NetModular.Module.Admin.Domain.LoginLog
{
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
        GitHub
    }
}
