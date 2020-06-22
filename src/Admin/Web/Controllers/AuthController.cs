using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Abstractions.LoginModels;
using NetModular.Lib.Auth.Web;
using NetModular.Lib.Auth.Web.Attributes;
using NetModular.Lib.Utils.Mvc.Helpers;
using NetModular.Module.Admin.Application.AuthService;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("身份认证")]
    public class AuthController : Web.ModuleController
    {
        private readonly IAuthService _service;
        private readonly ILoginHandler _loginHandler;
        private readonly IpHelper _ipHelper;
        private readonly ILoginClaimsExtendProvider _claimsExtendProvider;

        public AuthController(IAuthService service, ILoginHandler loginHandler, IpHelper ipHelper, ILoginClaimsExtendProvider claimsExtendProvider)
        {
            _service = service;
            _loginHandler = loginHandler;
            _ipHelper = ipHelper;
            _claimsExtendProvider = claimsExtendProvider;
        }

        [HttpGet]
        [AllowAnonymous]
        [DisableAuditing]
        [Description("获取验证码")]
        public IResultModel VerifyCode(int length = 6)
        {
            return _service.CreateVerifyCode(length);
        }

        [HttpPost]
        [AllowAnonymous]
        [DisableAuditing]
        [Description("用户名登录")]
        public async Task<IResultModel> Login(UserNameLoginModel model)
        {
            model.IP = _ipHelper.IP;
            model.UserAgent = _ipHelper.UserAgent;

            var result = await _service.Login(model);
            return LoginHandle(result);
        }

        [HttpPost("email")]
        [AllowAnonymous]
        [DisableAuditing]
        [Description("邮箱登录")]
        public async Task<IResultModel> Login(EmailLoginModel model)
        {
            model.IP = _ipHelper.IP;
            model.UserAgent = _ipHelper.UserAgent;

            var result = await _service.Login(model);
            return LoginHandle(result);
        }

        [HttpPost("username_or_email")]
        [AllowAnonymous]
        [DisableAuditing]
        [Description("用户名或邮箱登录")]
        public async Task<IResultModel> Login(UserNameOrEmailLoginModel model)
        {
            model.IP = _ipHelper.IP;
            model.UserAgent = _ipHelper.UserAgent;

            var result = await _service.Login(model);
            return LoginHandle(result);
        }

        [HttpPost("phone/send_verify_code")]
        [AllowAnonymous]
        [DisableAuditing]
        [Description("发送手机验证码")]
        public Task<IResultModel> Login(PhoneVerifyCodeSendModel model)
        {
            return _service.SendPhoneVerifyCode(model);
        }

        [HttpPost("phone")]
        [AllowAnonymous]
        [DisableAuditing]
        [Description("手机号登录")]
        public async Task<IResultModel> Login(PhoneLoginModel model)
        {
            model.IP = _ipHelper.IP;
            model.UserAgent = _ipHelper.UserAgent;

            var result = await _service.Login(model);
            return LoginHandle(result);
        }

        [HttpPost("custom")]
        [AllowAnonymous]
        [DisableAuditing]
        [Description("自定义登录")]
        public async Task<IResultModel> Custom(CustomLoginModel model)
        {
            model.IP = _ipHelper.IP;
            model.UserAgent = _ipHelper.UserAgent;

            var result = await _service.Login(model);
            return LoginHandle(result);
        }

        /// <summary>
        /// 登录处理
        /// </summary>
        private IResultModel LoginHandle(LoginResultModel result)
        {
            if (result.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsName.TenantId, result.TenantId != null ? result.TenantId.ToString() : ""),
                    new Claim(ClaimsName.AccountId, result.AccountId.ToString()),
                    new Claim(ClaimsName.AccountName, result.Name),
                    new Claim(ClaimsName.AccountType, result.AccountType.ToInt().ToString()),
                    new Claim(ClaimsName.Platform, result.Platform.ToInt().ToString()),
                    new Claim(ClaimsName.LoginTime, result.LoginTime.ToTimestamp().ToString())
                };

                //自定义扩展Claims
                var extendClaims = _claimsExtendProvider.GetExtendClaims(result.AccountId);
                if (extendClaims != null && extendClaims.Any())
                    claims.AddRange(extendClaims);

                return _loginHandler.Hand(claims, result.RefreshToken);
            }

            return ResultModel.Failed(result.Error);
        }

        [HttpGet]
        [AllowAnonymous]
        [DisableAuditing]
        [Description("刷新令牌")]
        public async Task<IResultModel> RefreshToken([BindRequired] string refreshToken)
        {
            var result = await _service.RefreshToken(refreshToken);
            return LoginHandle(result);
        }

        [HttpGet]
        [Common]
        [Description("获取认证信息")]
        public Task<IResultModel> AuthInfo()
        {
            return _service.GetAuthInfo();
        }
    }
}
