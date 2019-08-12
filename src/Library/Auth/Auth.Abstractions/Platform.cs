using System.ComponentModel;

namespace Tm.Lib.Auth.Abstractions
{
    /// <summary>
    /// 平台类型
    /// </summary>
    public enum Platform
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnown = -1,
        /// <summary>
        /// Web
        /// </summary>
        [Description("Web")]
        Web,
        /// <summary>
        /// 安卓
        /// </summary>
        [Description("安卓")]
        Android,
        /// <summary>
        /// IOS
        /// </summary>
        [Description("IOS")]
        IOS
    }
}
