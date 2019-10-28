using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.SystemService.ViewModels
{
    public class SystemConfigModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "请输入系统标题")]
        public string Title { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// LogoUrl
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// 首页地址
        /// </summary>
        public string Home { get; set; }

        /// <summary>
        /// 个人信息页
        /// </summary>
        public string UserInfoPage { get; set; }

        /// <summary>
        /// 启用按钮权限
        /// </summary>
        public bool ButtonPermission { get; set; }

        /// <summary>
        /// 审计日志
        /// </summary>
        public bool Auditing { get; set; }

        /// <summary>
        /// 开启权限验证
        /// </summary>
        public bool PermissionValidate { get; set; }

        /// <summary>
        /// 工具栏
        /// </summary>
        public SystemToolbar Toolbar { get; set; } = new SystemToolbar();

        /// <summary>
        /// 自定义Css样式
        /// </summary>
        public string CustomCss { get; set; }

        /// <summary>
        /// 菜单只能打开一个
        /// </summary>
        public bool MenuUniqueOpened { get; set; }

        /// <summary>
        /// 设置对话框是否可以点击模态框关闭
        /// </summary>
        public bool DialogCloseOnClickModal { get; set; }

        /// <summary>
        /// 登录配置项
        /// </summary>
        public SystemLoginOptions LoginOptions { get; set; } = new SystemLoginOptions();
    }

    /// <summary>
    /// 系统工具栏
    /// </summary>
    public class SystemToolbar
    {
        /// <summary>
        /// 全屏
        /// </summary>
        public bool Fullscreen { get; set; }

        /// <summary>
        /// 皮肤设置
        /// </summary>
        public bool Skin { get; set; }

        /// <summary>
        /// 退出
        /// </summary>
        public bool Logout { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public bool UserInfo { get; set; }
    }

    /// <summary>
    /// 系统登录设置
    /// </summary>
    public class SystemLoginOptions
    {
        /// <summary>
        /// 登录页类型
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// 启用验证码功能
        /// </summary>
        public bool VerifyCode { get; set; }
    }

    /// <summary>
    /// 系统配置键
    /// </summary>
    public class SystemConfigKey
    {
        /// <summary>
        /// 系统标题
        /// </summary>
        public const string Title = "sys_title";

        /// <summary>
        /// 系统logo
        /// </summary>
        public const string Logo = "sys_logo";

        /// <summary>
        /// 系统首页
        /// </summary>
        public const string Home = "sys_home";

        /// <summary>
        /// 个人信息页
        /// </summary>
        public const string UserInfoPage = "sys_userinfo_page";

        /// <summary>
        /// 是否启用按钮权限
        /// </summary>
        public const string ButtonPermission = "sys_button_permission";

        /// <summary>
        /// 是否启用权限验证
        /// </summary>
        public const string PermissionValidate = "sys_permission_validate";

        /// <summary>
        /// 是否启用审计功能
        /// </summary>
        public const string Auditing = "sys_auditing";

        /// <summary>
        /// 工具栏：全屏按钮
        /// </summary>
        public const string ToolbarFullscreen = "sys_toolbar_fullscreen";

        /// <summary>
        /// 工具栏：皮肤切换
        /// </summary>
        public const string ToolbarSkin = "sys_toolbar_skin";

        /// <summary>
        /// 工具栏：退出
        /// </summary>
        public const string ToolbarLogout = "sys_toolbar_logout";

        /// <summary>
        /// 工具栏：用户信息
        /// </summary>
        public const string ToolbarUserInfo = "sys_toolbar_userinfo";

        /// <summary>
        /// 自定义css样式
        /// </summary>
        public const string CustomCss = "sys_toolbar_customcss";

        /// <summary>
        /// 菜单只能打开一个
        /// </summary>
        public const string MenuUniqueOpened = "sys_menu_unique_opened";

        /// <summary>
        /// 点击模态框关闭对话框
        /// </summary>
        public const string DialogCloseOnClickModal = "sys_dialog_close_on_click_modal";

        /// <summary>
        /// 登录页面类型
        /// </summary>
        public const string LoginOptionsType = "sys_login_options_type";

        /// <summary>
        /// 启用登录验证码功能
        /// </summary>
        public const string LoginOptionsVerifyCode = "sys_login_options_verify_code";

    }
}
