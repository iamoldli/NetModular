using System.ComponentModel;

namespace Tm.Module.PersonnelFiles.Domain.User
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Sex
    {
        [Description("未知")]
        UnKnown,
        [Description("男")]
        Boy,
        [Description("女")]
        Girl
    }
}
