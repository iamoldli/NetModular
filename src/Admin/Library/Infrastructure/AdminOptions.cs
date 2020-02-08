using NetModular.Lib.Options.Abstraction;

namespace NetModular.Module.Admin.Infrastructure
{
    /// <summary>
    /// 权限管理配置项
    /// </summary>
    public class AdminOptions : IModuleOptions
    {
        /// <summary>
        /// 启用审计日志
        /// </summary>
        [ModuleOptionDefinition("启用审计日志")]
        public bool AuditingEnabled { get; set; }

        /// <summary>
        /// JWT刷新令牌有效期
        /// </summary>
        [ModuleOptionDefinition("JWT刷新令牌有效期(天)")]
        public int RefreshTokenExpires { get; set; } = 7;

        /// <summary>
        /// 账户默认密码
        /// </summary>
        [ModuleOptionDefinition("账户默认密码")]
        public string DefaultPassword { get; set; } = "123456";
    }
}
