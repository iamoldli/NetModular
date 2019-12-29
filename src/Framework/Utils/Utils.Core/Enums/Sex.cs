using System.ComponentModel;

namespace NetModular.Lib.Utils.Core.Enums
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
        [Description("男")]
        Boy,
        [Description("女")]
        Girl
    }
}
