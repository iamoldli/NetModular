namespace NetModular.Module.Admin.Application.SystemService.ViewModels
{
    /// <summary>
    /// 系统配置信息
    /// </summary>
    public class SystemConfigModel
    {
        /// <summary>
        /// 基础信息
        /// </summary>
        public SystemBaseConfigModel Base { get; } = new SystemBaseConfigModel();

        /// <summary>
        /// 权限信息
        /// </summary>
        public SystemPermissionConfigModel Permission { get; } = new SystemPermissionConfigModel();

        /// <summary>
        /// 登录信息
        /// </summary>
        public SystemLoginConfigModel Login { get; } = new SystemLoginConfigModel();

        /// <summary>
        /// 工具栏信息
        /// </summary>
        public SystemToolbarConfigModel Toolbar { get; } = new SystemToolbarConfigModel();

        /// <summary>
        /// 组件信息
        /// </summary>
        public SystemComponentConfigModel Component { get; } = new SystemComponentConfigModel();
    }
}
