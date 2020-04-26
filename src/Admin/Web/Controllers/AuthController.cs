using System.ComponentModel;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Web;
using NetModular.Lib.Auth.Web.Attributes;
using NetModular.Lib.Module.AspNetCore.Attributes;
using NetModular.Lib.Utils.Mvc.Helpers;
using NetModular.Module.Admin.Application.AuthService;
using NetModular.Module.Admin.Application.AuthService.ResultModels;
using NetModular.Module.Admin.Application.AuthService.ViewModels;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("身份认证")]
    public class AuthController : Web.ModuleController
    {
        private readonly IAuthService _service;
        private readonly ILoginHandler _loginHandler;
        private readonly IpHelper _ipHelper;
        
        public AuthController(IAuthService service, ILoginHandler loginHandler, IpHelper ipHelper)
        {
            _service = service;
            _loginHandler = loginHandler;
            _ipHelper = ipHelper;
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
            return LoginHandle(result, model.Platform);
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
            return LoginHandle(result, model.Platform);
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
            return LoginHandle(result, model.Platform);
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
        [Description("手机号登录登录")]
        public async Task<IResultModel> Login(PhoneLoginModel model)
        {
            model.IP = _ipHelper.IP;
            model.UserAgent = _ipHelper.UserAgent;

            var result = await _service.Login(model);
            return LoginHandle(result, model.Platform);
        }

        /// <summary>
        /// 登录处理
        /// </summary>
        private IResultModel LoginHandle(ResultModel<LoginResultModel> result, Platform platform)
        {
            if (result.Successful)
            {
                var account = result.Data.Account;
                var loginInfo = result.Data.AuthInfo;
                var claims = new[]
                {
                    new Claim(ClaimsName.AccountId, account.Id.ToString()),
                    new Claim(ClaimsName.AccountName, account.Name),
                    new Claim(ClaimsName.AccountType, account.Type.ToInt().ToString()),
                    new Claim(ClaimsName.Platform, platform.ToInt().ToString()),
                    new Claim(ClaimsName.LoginTime, loginInfo.LoginTime.ToString())
                };

                return _loginHandler.Hand(claims, loginInfo.RefreshToken);
            }

            return ResultModel.Failed(result.Msg);
        }

        [HttpGet]
        [AllowAnonymous]
        [DisableAuditing]
        [Description("刷新令牌")]
        public async Task<IResultModel> RefreshToken([BindRequired]string refreshToken)
        {
            var result = await _service.RefreshToken(refreshToken);
            if (result.Successful)
            {
                var account = result.Data.Account;
                var loginInfo = result.Data.AuthInfo;
                var claims = new[]
                {
                    new Claim(ClaimsName.AccountId, account.Id.ToString()),
                    new Claim(ClaimsName.AccountName, account.Name),
                    new Claim(ClaimsName.AccountType, account.Type.ToInt().ToString()),
                    new Claim(ClaimsName.Platform, loginInfo.Platform.ToInt().ToString()),
                    new Claim(ClaimsName.LoginTime, loginInfo.LoginTime.ToString())
                };

                return _loginHandler.Hand(claims, loginInfo.RefreshToken);
            }

            return ResultModel.Failed(result.Msg);
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
