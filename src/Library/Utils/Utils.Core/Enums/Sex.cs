using System.ComponentModel;

namespace Nm.Lib.Utils.Core.Enums
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Sex
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnown = -1,
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Boy,
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Girl
    }
}
