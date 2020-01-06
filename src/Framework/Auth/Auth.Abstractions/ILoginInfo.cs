using System;

namespace NetModular.Lib.Auth.Abstractions
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public interface ILoginInfo
    {
        /// <summary>
        /// 账户编号
        /// </summary>
        Guid AccountId { get; }

        /// <summary>
        /// 账户名称
        /// </summary>
        string AccountName { get; }

        /// <summary>
        /// 账户类型
        /// </summary>
        AccountType AccountType { get; }

        /// <summary>
        /// 平台
        /// </summary>
        Platform Platform { get; }

        /// <summary>
        /// 获取当前用户IP(包含IPv和IPv6)
        /// </summary>
        string IP { get; }

        /// <summary>
        /// 获取当前用户IPv4
        /// </summary>
        string IPv4 { get; }

        /// <summary>
        /// 获取当前用户IPv6
        /// </summary>
        string IPv6 { get; }
    }
}
