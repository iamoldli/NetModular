namespace NetModular.Lib.Utils.Core.SystemConfig
{
    /// <summary>
    /// 系统权限配置信息
    /// </summary>
    public class SystemPermissionConfigModel
    {
        /// <summary>
        /// 开启权限验证
        /// </summary>
        [ConfigDescription("sys_permission_validate", "开启权限验证")]
        public bool Validate { get; set; }

        /// <summary>
        /// 启用按钮权限
        /// </summary>
        [ConfigDescription("sys_permission_button", "启用按钮权限")]
        public bool Button { get; set; }

        /// <summary>
        /// 审计日志
        /// </summary>
        [ConfigDescription("sys_permission_auditing", "审计日志")]
        public bool Auditing { get; set; }

        /// <summary>
        /// 单账户登录
        /// </summary>
        [ConfigDescription("sys_permission_single_account_login", "单账户登录")]
        public bool SingleAccountLogin { get; set; }

        /// <summary>
        /// JWT刷新令牌有效期
        /// </summary>
        [ConfigDescription("sys_permission_refresh_token_expires", "JWT刷新令牌有效期(天)")]
        public int RefreshTokenExpires { get; set; } = 7;

        /// <summary>
        /// 账户默认密码
        /// </summary>
        [ConfigDescription("sys_permission_default_password", "账户默认密码")]
        public string DefaultPassword { get; set; } = "123456";
    }
}
