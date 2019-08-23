using System.ComponentModel;

namespace Nm.Lib.Auth.Abstractions
{
    /// <summary>
    /// 账户类型
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// 管理员
        /// </summary>
        [Description("管理员")]
        Admin,
        /// <summary>
        /// 人员
        /// </summary>
        [Description("人员")]
        User,
        /// <summary>
        /// 企业
        /// </summary>
        [Description("企业")]
        Enterprise
    }
}
