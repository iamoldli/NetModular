using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Config.Abstractions;
using NetModular.Module.Admin.Application.AuthService.Interfaces;
using NetModular.Module.Admin.Application.AuthService.LoginHandler;
using NetModular.Module.Admin.Application.AuthService.ResultModels;
using NetModular.Module.Admin.Application.AuthService.ViewModels;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.AccountAuthInfo;
using NetModular.Module.Admin.Domain.AccountConfig;
using NetModular.Module.Admin.Domain.Button;
using NetModular.Module.Admin.Domain.Menu;
using NetModular.Module.Admin.Infrastructure;

namespace NetModular.Module.Admin.Application.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly ICacheHandler _cacheHandler;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountAuthInfoRepository _authInfoRepository;
        private readonly IAccountConfigRepository _configRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IButtonRepository _buttonRepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        private readonly ILoginInfo _loginInfo;
        private readonly IConfigProvider _configProvider;
        private readonly IVerifyCodeProvider _verifyCodeProvider;
        private readonly UserNameLoginHandler _userNameLoginHandler;
        private readonly EmailLoginHandler _emailLoginHandler;
        private readonly UserNameOrEmailLoginHandler _userNameOrEmailLoginHandler;
        private readonly PhoneLoginHandler _phoneLoginHandler;
        private readonly IPhoneVerifyCodeProvider _phoneVerifyCodeProvider;

        public AuthService(ICacheHandler cacheHandler, IAccountRepository accountRepository, IAccountAuthInfoRepository authInfoRepository, IAccountConfigRepository configRepository, IServiceProvider serviceProvider, IMenuRepository menuRepository, IMapper mapper, IButtonRepository buttonRepository, ILoginInfo loginInfo, IVerifyCodeProvider verifyCodeProvider, UserNameLoginHandler userNameLoginHandler, EmailLoginHandler emailLoginHandler, UserNameOrEmailLoginHandler userNameOrEmailLoginHandler, PhoneLoginHandler phoneLoginHandler, IPhoneVerifyCodeProvider phoneVerifyCodeProvider, IConfigProvider configProvider)
        {
            _cacheHandler = cacheHandler;
            _accountRepository = accountRepository;
            _authInfoRepository = authInfoRepository;
            _configRepository = configRepository;
            _serviceProvider = serviceProvider;
            _menuRepository = menuRepository;
            _mapper = mapper;
            _buttonRepository = buttonRepository;
            _loginInfo = loginInfo;
            _verifyCodeProvider = verifyCodeProvider;
            _userNameLoginHandler = userNameLoginHandler;
            _emailLoginHandler = emailLoginHandler;
            _userNameOrEmailLoginHandler = userNameOrEmailLoginHandler;
            _phoneLoginHandler = phoneLoginHandler;
            _phoneVerifyCodeProvider = phoneVerifyCodeProvider;
            _configProvider = configProvider;
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
        public Task<ResultModel<LoginResultModel>> Login(UserNameLoginModel model)
        {
            return _userNameLoginHandler.Handle(model);
        }

        /// <summary>
        /// 邮箱登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResultModel<LoginResultModel>> Login(EmailLoginModel model)
        {
            return _emailLoginHandler.Handle(model);
        }

        /// <summary>
        /// 用户名或邮箱登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResultModel<LoginResultModel>> Login(UserNameOrEmailLoginModel model)
        {
            return _userNameOrEmailLoginHandler.Handle(model);
        }

        public Task<ResultModel<LoginResultModel>> Login(PhoneLoginModel model)
        {
            return _phoneLoginHandler.Handle(model);
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
        public async Task<ResultModel<LoginResultModel>> RefreshToken(string refreshToken)
        {
            var result = new ResultModel<LoginResultModel>();
            var cacheKey = $"{CacheKeys.AUTH_REFRESH_TOKEN}:{refreshToken}";
            if (!_cacheHandler.TryGetValue(cacheKey, out AccountAuthInfoEntity authInfo))
            {
                authInfo = await _authInfoRepository.GetByRefreshToken(refreshToken);
                if (authInfo == null)
                    return result.Failed("身份认证信息无效，请重新登录~");

                //加入缓存
                var expires = (int)(authInfo.RefreshTokenExpiredTime - DateTime.Now).TotalMinutes;
                await _cacheHandler.SetAsync(cacheKey, authInfo, expires);
            }

            if (authInfo.RefreshTokenExpiredTime <= DateTime.Now)
                return result.Failed("身份认证信息过期，请重新登录~");

            var account = await _accountRepository.GetAsync(authInfo.AccountId);
            if (account == null)
                return result.Failed("账户信息不存在");

            var checkAccountResult = account.Check();
            if (!checkAccountResult.Successful)
                return result.Failed(checkAccountResult.Msg);

            return result.Success(new LoginResultModel
            {
                Account = account,
                AuthInfo = authInfo
            });
        }

        /// <summary>
        /// 获取认证信息
        /// </summary>
        /// <returns></returns>
        public async Task<IResultModel> GetAuthInfo()
        {
            var account = await _accountRepository.GetAsync(_loginInfo.AccountId);
            if (account == null)
                return ResultModel.Failed("账户信息不存在");

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
                        FontSize = ""
                    };
                }

                var getMenuTree = GetAccountMenuTree(_loginInfo.AccountId);
                var getButtonCodeList = _buttonRepository.QueryCodeByAccount(_loginInfo.AccountId);

                model.Menus = await getMenuTree;
                model.Buttons = await getButtonCodeList;

            }

            #endregion

            return ResultModel.Success(model);
        }

        #region ==获取账户的菜单树==

        /// <summary>
        /// 获取账户的菜单树
        /// </summary>
        /// <returns></returns>
        private async Task<List<AccountMenuItem>> GetAccountMenuTree(Guid accountId)
        {
            var entities = (await _menuRepository.GetByAccount(accountId)).Distinct(new MenuComparer()).ToList();
            var all = _mapper.Map<List<AccountMenuItem>>(entities);
            var tree = all.Where(e => e.ParentId.IsEmpty()).OrderBy(e => e.Sort).ToList();

            tree.ForEach(menu =>
            {
                if (menu.Type == MenuType.Node)
                    SetChildren(menu, all);
            });

            return tree;
        }

        private void SetChildren(AccountMenuItem parent, List<AccountMenuItem> all)
        {
            parent.Children = all.Where(e => e.ParentId == parent.Id).OrderBy(e => e.Sort).ToList();

            if (parent.Children.Any())
            {
                parent.Children.ForEach(menu =>
                {
                    if (menu.Type == MenuType.Node)
                        SetChildren(menu, all);
                });
            }
        }

        #endregion

        #region ==获取认证信息==

        public async Task<AccountAuthInfoEntity> GetAuthInfo(Guid accountId, Platform platform)
        {
            if (!_cacheHandler.TryGetValue($"{CacheKeys.ACCOUNT_AUTH_INFO}:{accountId}:{platform.ToInt()}", out AccountAuthInfoEntity authInfo))
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
