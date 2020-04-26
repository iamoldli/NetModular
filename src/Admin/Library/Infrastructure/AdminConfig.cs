using NetModular.Lib.Config.Abstractions;

namespace NetModular.Module.Admin.Infrastructure
{
    /// <summary>
    /// 权限管理配置项
    /// </summary>
    public class AdminConfig : IConfig
    {
        /// <summary>
        /// 启用审计日志
        /// </summary>
        public bool Auditing { get; set; }

        /// <summary>
        /// 启用登录日志
        /// </summary>
        public bool LoginLog { get; set; }

        /// <summary>
        /// 账户默认密码
        /// </summary>
        public string DefaultPassword { get; set; } = "123456";
    }
}
