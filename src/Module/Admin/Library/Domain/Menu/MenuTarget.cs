using System.ComponentModel;

namespace Tm.Module.Admin.Domain.Menu
{
    /// <summary>
    /// 菜单打开方式(只针对链接菜单)
    /// </summary>
    public enum MenuTarget
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnown = -1,
        /// <summary>
        /// 新窗口
        /// </summary>
        [Description("新窗口")]
        Blank,
        /// <summary>
        /// 当前窗口
        /// </summary>
        [Description("当前窗口")]
        Self,
        /// <summary>
        /// 对话框
        /// </summary>
        [Description("对话框")]
        Dialog,
        /// <summary>
        /// 容器内
        /// </summary>
        [Description("容器内")]
        Container,
        /// <summary>
        /// 内容区
        /// </summary>
        [Description("内容区")]
        Content
    }
}
