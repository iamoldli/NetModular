using NetModular.Lib.Utils.Core.Options;

namespace NetModular.Module.Admin.Infrastructure.Options
{
    /// <summary>
    /// 权限管理配置项
    /// </summary>
    public class AdminOptions : IModuleOptions
    {
        /// <summary>
        /// 是否开启审计功能
        /// </summary>
        public bool Auditing { get; set; }

        /// <summary>
        /// 是否开启权限验证
        /// </summary>
        public bool PermissionValidate { get; set; }

        public AdminOptions()
        {
            Auditing = true;
            PermissionValidate = true;
        }
    }
}
