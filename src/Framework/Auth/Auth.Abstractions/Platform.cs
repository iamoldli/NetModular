using System.ComponentModel;

namespace NetModular.Lib.Auth.Abstractions
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
        /// Android
        /// </summary>
        [Description("安卓")]
        Android,
        /// <summary>
        /// IOS
        /// </summary>
        [Description("IOS")]
        IOS,
        /// <summary>
        /// PC
        /// </summary>
        [Description("PC")]
        PC,
        /// <summary>
        /// Mobile
        /// </summary>
        [Description("手机")]
        Mobile,
        /// <summary>
        /// WeChat
        /// </summary>
        [Description("微信")]
        WeChat,
        /// <summary>
        /// 小程序
        /// </summary>
        [Description("小程序")]
        MiniProgram,
        /// <summary>
        /// 支付宝
        /// </summary>
        [Description("支付宝")]
        Alipay
    }
}
