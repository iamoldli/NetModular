using NetModular.Lib.Config.Abstractions;

namespace NetModular.Lib.Auth.Abstractions
{
    /// <summary>
    /// 身份认证和授权配置
    /// </summary>
    public class AuthConfig : IConfig
    {
        /// <summary>
        /// 开启权限验证
        /// </summary>
        public bool Validate { get; set; }

        /// <summary>
        /// 开启按钮权限
        /// </summary>
        public bool Button { get; set; }

        /// <summary>
        /// 单账户登录
        /// </summary>
        public bool SingleAccount { get; set; }

        /// <summary>
        /// Jwt配置
        /// </summary>
        public JwtConfig Jwt { get; set; } = new JwtConfig();
    }

    /// <summary>
    /// JWT配置
    /// </summary>
    public class JwtConfig
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; } = "twAJ$j5##pVc5*y&";

        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; } = "http://127.0.0.1:6220";

        /// <summary>
        /// 消费者
        /// </summary>
        public string Audience { get; set; } = "http://127.0.0.1:6220";

        /// <summary>
        /// 有效期(分钟，默认120)
        /// </summary>
        public int Expires { get; set; } = 120;

        /// <summary>
        /// 刷新令牌有效期(单位：天，默认7)
        /// </summary>
        public int RefreshTokenExpires { get; set; } = 7;
    }
}
