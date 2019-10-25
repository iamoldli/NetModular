using System.ComponentModel;

namespace NetModular.Lib.Auth.Abstractions
{
    /// <summary>
    /// 账户类型
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnown = -1,
        /// <summary>
        /// 管理员
        /// </summary>
        [Description("管理员")]
        Admin,
        /// <summary>
        /// 人员
        /// </summary>
        [Description("个人")]
        User,
        /// <summary>
        /// 企业
        /// </summary>
        [Description("企业")]
        Enterprise
    }
}
