namespace NetModular.Lib.Utils.Core.SystemConfig
{
    /// <summary>
    /// 系统配置信息
    /// </summary>
    public class SystemConfigModel
    {
        /// <summary>
        /// 基础信息
        /// </summary>
        public SystemBaseConfigModel Base { get; set; } = new SystemBaseConfigModel();

        /// <summary>
        /// 权限信息
        /// </summary>
        public SystemPermissionConfigModel Permission { get; set; } = new SystemPermissionConfigModel();

        /// <summary>
        /// 登录信息
        /// </summary>
        public SystemLoginConfigModel Login { get; set; } = new SystemLoginConfigModel();

        /// <summary>
        /// 工具栏信息
        /// </summary>
        public SystemToolbarConfigModel Toolbar { get; set; } = new SystemToolbarConfigModel();

        /// <summary>
        /// 组件信息
        /// </summary>
        public SystemComponentConfigModel Component { get; set; } = new SystemComponentConfigModel();
    }
}
