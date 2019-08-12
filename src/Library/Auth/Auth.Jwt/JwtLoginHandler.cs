using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Tm.Lib.Auth.Web;
using Tm.Lib.Utils.Core.Result;

namespace Tm.Lib.Auth.Jwt
{
    public class JwtLoginHandler : ILoginHandler
    {
        private readonly ILogger _logger;
        private readonly JwtOptions _options;

        public JwtLoginHandler(JwtOptions options, ILogger<JwtLoginHandler> logger)
        {
            _options = options;
            _logger = logger;
        }

        public IResultModel Hand(Claim[] claims)
        {
            var token = Build(claims);

            _logger.LogDebug("生成JwtToken：{token}", token);

            var model = new JwtTokenModel
            {
                AccessToken = token
            };

            return ResultModel.Success(model);
        }

        private string Build(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_options.Issuer,
                _options.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_options.Expires),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
