using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Web;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Utils.Core.Attributes;

namespace NetModular.Lib.Auth.Jwt
{
    [Singleton]
    public class JwtLoginHandler : ILoginHandler
    {
        private readonly ILogger _logger;
        private readonly IConfigProvider _configProvider;

        public JwtLoginHandler(ILogger<JwtLoginHandler> logger, IConfigProvider configProvider)
        {
            _logger = logger;
            _configProvider = configProvider;
        }

        public IResultModel Hand(List<Claim> claims, string extendData)
        {
            var options = _configProvider.Get<AuthConfig>().Jwt;

            var token = Build(claims, options);

            _logger.LogDebug("生成JwtToken：{token}", token);

            var model = new JwtTokenModel
            {
                AccessToken = token,
                ExpiresIn = options.Expires * 60,
                RefreshToken = extendData
            };

            return ResultModel.Success(model);
        }

        private string Build(List<Claim> claims, JwtConfig config)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(config.Issuer, config.Audience, claims, DateTime.Now, DateTime.Now.AddMinutes(config.Expires), signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
