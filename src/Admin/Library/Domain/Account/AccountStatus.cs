using System.ComponentModel;

namespace NetModular.Module.Admin.Domain.Account
{
    /// <summary>
    /// 账户状态
    /// </summary>
    public enum AccountStatus
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnown = -1,
        /// <summary>
        /// 注册
        /// </summary>
        [Description("注册")]
        Register,
        /// <summary>
        /// 激活
        /// </summary>
        [Description("激活")]
        Active,
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disabled,
        /// <summary>
        /// 注销
        /// </summary>
        [Description("注销")]
        Logout
    }
}
