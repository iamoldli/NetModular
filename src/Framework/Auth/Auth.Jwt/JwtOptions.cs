namespace Nm.Lib.Auth.Jwt
{
    /// <summary>
    /// Jwt配置项
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 消费者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 有效期(分钟)
        /// </summary>
        public int Expires { get; set; } = 120;
    }
}
