namespace NetModular.Lib.Config.Abstractions.Impl
{
    /// <summary>
    /// 前端组件配置
    /// </summary>
    public class ComponentConfig : IConfig
    {
        /// <summary>
        /// 登录组件
        /// </summary>
        public Login Login { get; set; } = new Login();

        /// <summary>
        /// 菜单组件
        /// </summary>
        public Menu Menu { get; set; } = new Menu();

        /// <summary>
        /// 对话框组件
        /// </summary>
        public Dialog Dialog { get; set; } = new Dialog();

        /// <summary>
        /// 列表页组件
        /// </summary>
        public List List { get; set; } = new List();

        /// <summary>
        /// 标签导航
        /// </summary>
        public Tabnav Tabnav { get; set; } = new Tabnav();

        /// <summary>
        /// 工具栏
        /// </summary>
        public Toolbar Toolbar { get; set; } = new Toolbar();

        /// <summary>
        /// 自定义样式
        /// </summary>
        public string CustomCss { get; set; }
    }

    /// <summary>
    /// 登录组件
    /// </summary>
    public class Login
    {
        /// <summary>
        /// 登录页类型
        /// </summary>
        public string PageType { get; set; }

        /// <summary>
        /// 启用验证码功能
        /// </summary>
        public bool VerifyCode { get; set; }
    }

    /// <summary>
    /// 菜单组件
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// 是否只保持一个子菜单的展开
        /// </summary>
        public bool UniqueOpened { get; set; }
    }

    /// <summary>
    /// 对话框组件
    /// </summary>
    public class Dialog
    {
        /// <summary>
        /// 是否可以点击模态框关闭
        /// </summary>
        public bool CloseOnClickModal { get; set; }

        /// <summary>
        /// 是否可以点击模态框关闭
        /// </summary>
        public bool Draggable { get; set; }
    }

    /// <summary>
    /// 列表页配置信息
    /// </summary>
    public class List
    {
        /// <summary>
        /// 序号名称
        /// </summary>
        public string SerialNumberName { get; set; }
    }

    /// <summary>
    /// 标签导航组件配置信息
    /// </summary>
    public class Tabnav
    {
        /// <summary>
        /// 是否启用标签导航
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 是否显示首页
        /// </summary>
        public bool ShowHome { get; set; } = true;

        /// <summary>
        /// 首页地址
        /// </summary>
        public string HomeUrl { get; set; }

        /// <summary>
        /// 是否显示图标
        /// </summary>
        public bool ShowIcon { get; set; } = true;

        /// <summary>
        /// 最大页面数量
        /// </summary>
        public int MaxOpenCount { get; set; } = 20;
    }

    /// <summary>
    /// 工具栏
    /// </summary>
    public class Toolbar
    {
        /// <summary>
        /// 全屏
        /// </summary>
        public bool Fullscreen { get; set; } = true;

        /// <summary>
        /// 皮肤设置
        /// </summary>
        public bool Skin { get; set; } = true;

        /// <summary>
        /// 退出
        /// </summary>
        public bool Logout { get; set; } = true;

        /// <summary>
        /// 用户信息
        /// </summary>
        public bool UserInfo { get; set; } = true;
    }
}
