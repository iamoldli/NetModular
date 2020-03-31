using NetModular.Lib.Utils.Core.SystemConfig;
// ReSharper disable once CheckNamespace
namespace NetModular
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
    }
}
