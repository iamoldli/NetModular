using NetModular.Lib.Module.Abstractions;

namespace NetModular.Module.Admin.Web
{
    /// <summary>
    /// 权限管理配置项
    /// </summary>
    public class AdminOptions : IModuleOptions
    {
        /// <summary>
        /// 是否开启审计功能
        /// </summary>
        public bool Auditing { get; set; } = true;

        /// <summary>
        /// 是否开启权限验证
        /// </summary>
        public bool PermissionValidate { get; set; } = true;

        /// <summary>
        /// 文件上传路径
        /// </summary>
        public string UploadPath { get; set; }
    }
}
