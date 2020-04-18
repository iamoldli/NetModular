using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Config.Abstractions;

namespace NetModular.Lib.Auth.Jwt
{
    /// <summary>
    /// 因为签名的key是动态的，所以需要自定义jwt令牌验证处理器
    /// </summary>
    public class MyJwtSecurityTokenHandler : JwtSecurityTokenHandler, ISecurityTokenValidator
    {
        private readonly IConfigProvider _configProvider;

        public MyJwtSecurityTokenHandler(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters,
            out SecurityToken validatedToken)
        {
            var jwtConfig = _configProvider.Get<AuthConfig>().Jwt;

            validationParameters.ValidIssuer = jwtConfig.Issuer;
            validationParameters.ValidAudience = jwtConfig.Audience;
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));

            return base.ValidateToken(token, validationParameters, out validatedToken);
        }
    }
}
