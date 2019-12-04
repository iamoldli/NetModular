namespace NetModular.Module.Admin.Application.SystemService.ViewModels
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
    }
}
