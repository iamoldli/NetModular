namespace NetModular.Lib.Auth.Jwt
{
    /// <summary>
    /// JWT令牌
    /// </summary>
    public class JwtTokenModel
    {
        /// <summary>
        /// jwt令牌
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
