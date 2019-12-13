namespace NetModular.Lib.Utils.Core.SystemConfig
{
    /// <summary>
    /// 系统登录配置信息
    /// </summary>
    public class SystemLoginConfigModel
    {
        /// <summary>
        /// 登录页类型
        /// </summary>
        [ConfigDescription("sys_login_type", "登录页类型")]
        public string Type { get; set; }

        /// <summary>
        /// 启用验证码功能
        /// </summary>
        [ConfigDescription("sys_login_verify_code", "启用验证码功能")]
        public bool VerifyCode { get; set; }
    }
}
