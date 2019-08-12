using System.ComponentModel;

namespace Tm.Module.Admin.Domain.Menu
{
    /// <summary>
    /// 菜单类型
    /// </summary>
    public enum MenuType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnown = -1,
        /// <summary>
        /// 节点
        /// </summary>
        [Description("节点")]
        Node,
        /// <summary>
        /// 路由菜单
        /// </summary>
        [Description("路由")]
        Route,
        /// <summary>
        /// 链接菜单
        /// </summary>
        [Description("链接")]
        Link
    }
}
