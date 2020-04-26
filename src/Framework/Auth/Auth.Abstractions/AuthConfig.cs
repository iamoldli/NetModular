using System.ComponentModel;
using NetModular.Lib.Config.Abstractions;

namespace NetModular.Lib.Auth.Abstractions
{
    /// <summary>
    /// 身份认证和授权配置
    /// </summary>
    public class AuthConfig : IConfig
    {
        /// <summary>
        /// 启用验证码功能
        /// </summary>
        public bool VerifyCode { get; set; }

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

        /// <summary>
        /// 登录
        /// </summary>
        public LoginMode LoginMode { get; set; } = new LoginMode();
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

    /// <summary>
    /// 登录方式
    /// </summary>
    public class LoginMode
    {
        /// <summary>
        /// 用户名登录
        /// </summary>
        public bool UserName { get; set; } = true;

        /// <summary>
        /// 邮箱登录
        /// </summary>
        public bool Email { get; set; }

        /// <summary>
        /// 用户名或邮箱登录
        /// </summary>
        public bool UserNameOrEmail { get; set; }

        /// <summary>
        /// 手机号登录
        /// </summary>
        public bool Phone { get; set; }

        /// <summary>
        /// 微信登录
        /// </summary>
        public bool WeChat { get; set; }

        /// <summary>
        /// QQ登录
        /// </summary>
        public bool QQ { get; set; }

        /// <summary>
        /// GitHub
        /// </summary>
        public bool GitHub { get; set; }
    }
}
