using System.ComponentModel;

namespace Nm.Lib.Utils.Core.Enums
{
    /// <summary>
    /// 是否
    /// </summary>
    public enum Whether
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnown = -1,
        /// <summary>
        /// 否
        /// </summary>
        [Description("否")]
        False,
        /// <summary>
        /// 是
        /// </summary>
        [Description("是")]
        True,
    }
}
