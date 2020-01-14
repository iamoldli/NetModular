namespace NetModular.Lib.Utils.Core.SystemConfig
{
    /// <summary>
    /// 系统组件配置信息
    /// </summary>
    public class SystemComponentConfigModel
    {
        /// <summary>
        /// 菜单
        /// </summary>
        public MenuConfigModel Menu { get; } = new MenuConfigModel();

        /// <summary>
        /// 对话框
        /// </summary>
        public DialogConfigModel Dialog { get; } = new DialogConfigModel();

        /// <summary>
        /// 列表页
        /// </summary>
        public ListConfigModel List { get; } = new ListConfigModel();

        /// <summary>
        /// 导航页
        /// </summary>
        public TabnavConfigModel Tabnav { get; set; } = new TabnavConfigModel();

        /// <summary>
        /// 自定义Css样式
        /// </summary>
        [ConfigDescription("sys_component_custom_css", "自定义Css样式")]
        public string CustomCss { get; set; }
    }

    /// <summary>
    /// 菜单配置信息
    /// </summary>
    public class MenuConfigModel
    {
        /// <summary>
        /// 只能打开一个
        /// </summary>
        [ConfigDescription("sys_component_menu_unique_opened", "菜单只能打开一个")]
        public bool UniqueOpened { get; set; }
    }

    /// <summary>
    /// 对话框配置信息
    /// </summary>
    public class DialogConfigModel
    {
        /// <summary>
        /// 是否可以点击模态框关闭
        /// </summary>
        [ConfigDescription("sys_component_dialog_close_on_click_modal", "是否可以点击模态框关闭")]
        public bool CloseOnClickModal { get; set; }
    }

    /// <summary>
    /// 列表页配置信息
    /// </summary>
    public class ListConfigModel
    {
        /// <summary>
        /// 序号名称
        /// </summary>
        [ConfigDescription("sys_component_list_serial_number_name", "列表页序号名称")]
        public string SerialNumberName { get; set; }
    }

    /// <summary>
    /// 标签导航组件配置信息
    /// </summary>
    public class TabnavConfigModel
    {
        /// <summary>
        /// 序号名称
        /// </summary>
        [ConfigDescription("sys_component_tabnav_showicon", "标签导航是否显示图标")]
        public bool ShowIcon { get; set; }
    }
}
