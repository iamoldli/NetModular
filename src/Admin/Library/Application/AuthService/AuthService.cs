using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Abstractions.LoginHandlers;
using NetModular.Lib.Auth.Abstractions.LoginModels;
using NetModular.Lib.Auth.Abstractions.Providers;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Utils.Core.Helpers;
using NetModular.Module.Admin.Application.AuthService.LoginHandler;
using NetModular.Module.Admin.Application.AuthService.ResultModels;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.AccountAuthInfo;
using NetModular.Module.Admin.Domain.AccountConfig;
using NetModular.Module.Admin.Infrastructure;
using NetModular.Module.Admin.Infrastructure.AccountPermissionResolver;

namespace NetModular.Module.Admin.Application.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly ICacheHandler _cacheHandler;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountAuthInfoRepository _authInfoRepository;
        private readonly IAccountConfigRepository _configRepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILoginInfo _loginInfo;
        private readonly IConfigProvider _configProvider;
        private readonly IVerifyCodeProvider _verifyCodeProvider;
        private readonly IUserNameLoginHandler _userNameLoginHandler;
        private readonly IEmailLoginHandler _emailLoginHandler;
        private readonly IUserNameOrEmailLoginHandler _userNameOrEmailLoginHandler;
        private readonly IPhoneLoginHandler _phoneLoginHandler;
        private readonly ICustomLoginHandler _customLoginHandler;
        private readonly IPhoneVerifyCodeProvider _phoneVerifyCodeProvider;
        private readonly IAccountPermissionResolver _permissionResolver;
        private readonly DateTimeHelper _dateTimeHelper;

        public AuthService(ICacheHandler cacheHandler, IAccountRepository accountRepository, IAccountAuthInfoRepository authInfoRepository, IAccountConfigRepository configRepository, IServiceProvider serviceProvider, ILoginInfo loginInfo, IConfigProvider configProvider, IVerifyCodeProvider verifyCodeProvider, IUserNameLoginHandler userNameLoginHandler, IEmailLoginHandler emailLoginHandler, IUserNameOrEmailLoginHandler userNameOrEmailLoginHandler, IPhoneLoginHandler phoneLoginHandler, IPhoneVerifyCodeProvider phoneVerifyCodeProvider, IAccountPermissionResolver permissionResolver, ICustomLoginHandler customLoginHandler, DateTimeHelper dateTimeHelper)
        {
            _cacheHandler = cacheHandler;
            _accountRepository = accountRepository;
            _authInfoRepository = authInfoRepository;
            _configRepository = configRepository;
            _serviceProvider = serviceProvider;
            _loginInfo = loginInfo;
            _configProvider = configProvider;
            _verifyCodeProvider = verifyCodeProvider;
            _userNameLoginHandler = userNameLoginHandler;
            _emailLoginHandler = emailLoginHandler;
            _userNameOrEmailLoginHandler = userNameOrEmailLoginHandler;
            _phoneLoginHandler = phoneLoginHandler;
            _phoneVerifyCodeProvider = phoneVerifyCodeProvider;
            _permissionResolver = permissionResolver;
            _customLoginHandler = customLoginHandler;
            _dateTimeHelper = dateTimeHelper;
        }

        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public IResultModel CreateVerifyCode(int length = 6)
        {
            return ResultModel.Success(_verifyCodeProvider.Create(length));
        }

        /// <summary>
        /// 用户名登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<LoginResultModel> Login(UserNameLoginModel model)
        {
            return _userNameLoginHandler.Handle(model);
        }

        /// <summary>
        /// 邮箱登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<LoginResultModel> Login(EmailLoginModel model)
        {
            return _emailLoginHandler.Handle(model);
        }

        /// <summary>
        /// 用户名或邮箱登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<LoginResultModel> Login(UserNameOrEmailLoginModel model)
        {
            return _userNameOrEmailLoginHandler.Handle(model);
        }

        public Task<LoginResultModel> Login(PhoneLoginModel model)
        {
            return _phoneLoginHandler.Handle(model);
        }

        public Task<LoginResultModel> Login(CustomLoginModel model)
        {
            return _customLoginHandler.Handle(model);
        }

        public async Task<IResultModel> SendPhoneVerifyCode(PhoneVerifyCodeSendModel model)
        {
            var config = _configProvider.Get<AuthConfig>();
            if (!config.LoginMode.Phone)
                return ResultModel.Failed("未开启手机号登录方式");

            return await _phoneVerifyCodeProvider.Send(model.Phone, model.AreaCode);
        }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<LoginResultModel> RefreshToken(string refreshToken)
        {
            var result = new LoginResultModel();
            var cacheKey = CacheKeys.AUTH_REFRESH_TOKEN + refreshToken;
            if (!_cacheHandler.TryGetValue(cacheKey, out AccountAuthInfoEntity authInfo))
            {
                authInfo = await _authInfoRepository.GetByRefreshToken(refreshToken);
                if (authInfo == null)
                {
                    result.Error = "身份认证信息无效，请重新登录~";
                    return result;
                }

                //加入缓存
                var expires = (int)(authInfo.RefreshTokenExpiredTime - DateTime.Now).TotalMinutes;
                await _cacheHandler.SetAsync(cacheKey, authInfo, expires);
            }

            if (authInfo.RefreshTokenExpiredTime <= DateTime.Now)
            {
                result.Error = "身份认证信息过期，请重新登录~";
                return result;
            }

            var account = await _accountRepository.GetAsync(authInfo.AccountId);
            if (account == null)
            {
                result.Error = "账户信息不存在~";
                return result;
            }
            var checkAccountResult = account.Check();
            if (!checkAccountResult.Successful)
            {
                result.Error = checkAccountResult.Msg;
                return result;
            }

            result.Success = true;
            result.AccountId = account.Id;
            result.AccountType = account.Type;
            result.TenantId = account.TenantId;
            result.UserName = account.UserName;
            result.Name = account.Name;
            result.Phone = account.Phone;
            result.Platform = authInfo.Platform;
            result.RefreshToken = authInfo.RefreshToken;
            result.LoginTime = _dateTimeHelper.TimeStamp2DateTime(authInfo.LoginTime);
            return result;
        }

        /// <summary>
        /// 获取认证信息
        /// </summary>
        /// <returns></returns>
        public async Task<IResultModel> GetAuthInfo()
        {
            var account = await _accountRepository.GetAsync(_loginInfo.AccountId);
            if (account == null)
                return ResultModel.Failed("账户不存在");

            var result = account.Check();
            //检测账户状态
            if (!result.Successful)
                return result;

            var model = new AuthInfoModel
            {
                Id = account.Id,
                Type = account.Type,
                UserName = account.UserName,
                Name = account.Name,
                Phone = account.Phone,
                Email = account.Email,
            };

            #region ==获取账户详细信息==

            var detailsBuilders = _serviceProvider.GetServices<ILoginInfoDetailsBuilder>().ToList();
            if (detailsBuilders.Any())
            {
                var detailsBuilder = detailsBuilders.FirstOrDefault(m => m.AccountType == account.Type);
                if (detailsBuilder != null)
                {
                    model.Details = await detailsBuilder.Build(_loginInfo.AccountId);
                }
            }

            #endregion

            #region ==Web端==

            if (_loginInfo.Platform == Platform.Web)
            {
                //加载配置信息
                var config = await _configRepository.GetByAccount(_loginInfo.AccountId);
                if (config != null)
                {
                    model.Skin = new SkinConfigModel
                    {
                        Name = config.Skin,
                        Theme = config.Theme,
                        FontSize = config.FontSize
                    };
                }
                else
                {
                    model.Skin = new SkinConfigModel
                    {
                        Name = "pretty",
                        Theme = "",
                        FontSize = "small"
                    };
                }

                var getMenuTree = _permissionResolver.ResolveMenus(_loginInfo.AccountId);
                var getPageCodes = _permissionResolver.ResolvePages(_loginInfo.AccountId);
                var getButtonCodes = _permissionResolver.ResolveButtons(_loginInfo.AccountId);

                model.Menus = await getMenuTree;
                model.Pages = await getPageCodes;
                model.Buttons = await getButtonCodes;
            }

            #endregion

            return ResultModel.Success(model);
        }

        #region ==获取认证信息==

        public async Task<AccountAuthInfoEntity> GetAuthInfo(Guid accountId, Platform platform)
        {
            if (!_cacheHandler.TryGetValue($"{CacheKeys.ACCOUNT_AUTH_INFO}{accountId}:{platform.ToInt()}", out AccountAuthInfoEntity authInfo))
            {
                authInfo = await _authInfoRepository.Get(accountId, platform);
                if (authInfo == null)
                    return null;
            }

            return authInfo;
        }

        #endregion
    }
}
