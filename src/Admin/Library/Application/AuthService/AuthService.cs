using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Helpers;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Lib.Utils.Core.SystemConfig;
using NetModular.Module.Admin.Application.AuthService.ResultModels;
using NetModular.Module.Admin.Application.AuthService.ViewModels;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.AccountAuthInfo;
using NetModular.Module.Admin.Domain.AccountConfig;
using NetModular.Module.Admin.Domain.Button;
using NetModular.Module.Admin.Domain.Menu;
using NetModular.Module.Admin.Infrastructure;
using NetModular.Module.Admin.Infrastructure.PasswordHandler;
using NetModular.Module.Admin.Infrastructure.Repositories;

namespace NetModular.Module.Admin.Application.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DrawingHelper _drawingHelper;
        private readonly ICacheHandler _cacheHandler;
        private readonly SystemConfigModel _systemConfig;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountAuthInfoRepository _authInfoRepository;
        private readonly IAccountConfigRepository _configRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IButtonRepository _buttonRepository;
        private readonly AdminDbContext _dbContext;
        private readonly IPasswordHandler _passwordHandler;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        private readonly ILoginInfo _loginInfo;
        private readonly AdminOptions _options;

        public AuthService(DrawingHelper drawingHelper, ICacheHandler cacheHandler, SystemConfigModel systemConfig, IAccountRepository accountRepository, AdminDbContext dbContext, IAccountAuthInfoRepository authInfoRepository, IPasswordHandler passwordHandler, IAccountConfigRepository configRepository, IServiceProvider serviceProvider, IMenuRepository menuRepository, IMapper mapper, IButtonRepository buttonRepository, ILoginInfo loginInfo, AdminOptions options)
        {
            _drawingHelper = drawingHelper;
            _cacheHandler = cacheHandler;
            _systemConfig = systemConfig;
            _accountRepository = accountRepository;
            _dbContext = dbContext;
            _authInfoRepository = authInfoRepository;
            _passwordHandler = passwordHandler;
            _configRepository = configRepository;
            _serviceProvider = serviceProvider;
            _menuRepository = menuRepository;
            _mapper = mapper;
            _buttonRepository = buttonRepository;
            _loginInfo = loginInfo;
            _options = options;
        }

        #region ==创建验证码==

        public IResultModel CreateVerifyCode(int length = 6)
        {
            var verifyCodeModel = new VerifyCodeResultModel
            {
                Id = Guid.NewGuid().ToString("N"),
                Base64String = _drawingHelper.DrawVerifyCodeBase64String(out string code, length)
            };

            var key = string.Format(CacheKeys.VerifyCodeKey, verifyCodeModel.Id);

            //把验证码放到内存缓存中，有效期10分钟
            _cacheHandler.SetAsync(key, code, 10);

            return ResultModel.Success(verifyCodeModel);
        }

        public IResultModel CreateMobileCode(string code)
        {
            var id = Guid.NewGuid().ToString("N");
            var key = string.Format(CacheKeys.VerifyMobileCodeKey, id);

            //把验证码放到内存缓存中，有效期10分钟
            _cacheHandler.SetAsync(key, code, 10);
            return ResultModel.Success(new { Id = id });
        }

        #endregion

        #region ==登录认证==

        public async Task<ResultModel<LoginResultModel>> Login(LoginModel model)
        {
            var result = new ResultModel<LoginResultModel>();

            //检测验证码
            if (!await CheckVerifyCode(result, model))
                return result;

            //检测账户
            var account = await _accountRepository.GetByUserName(model.UserName, model.AccountType);
            var checkAccountResult = CheckAccount(account);
            if (!checkAccountResult.Successful)
                return result.Failed(checkAccountResult.Msg);

            //检测密码
            if (!CheckPassword(result, model, account))
                return result;

            using var uow = _dbContext.NewUnitOfWork();

            //判断是否激活，如果未激活需要修改为已激活状态
            if (account.Status == AccountStatus.Inactive)
            {
                if (!await _accountRepository.UpdateAccountStatus(account.Id, AccountStatus.Enabled, uow))
                {
                    return result.Failed();
                }
            }

            //更新登录信息
            var loginInfo = await UpdateLoginInfo(account, model, uow);
            if (loginInfo != null)
            {
                uow.Commit();

                if (_systemConfig.Login.VerifyCode)
                {
                    //删除验证码缓存
                    await _cacheHandler.RemoveAsync(string.Format(CacheKeys.VerifyCodeKey, model.VerifyCode.Id));
                }

                //清除账户的认证信息缓存
                await _cacheHandler.RemoveAsync(string.Format(CacheKeys.AccountAuthInfo, account.Id, model.Platform.ToInt()));

                return result.Success(new LoginResultModel
                {
                    Account = account,
                    AuthInfo = loginInfo
                });
            }

            return result.Failed();
        }


        public async Task<ResultModel<LoginResultModel>> LoginByMobileCode(LoginModel model)
        {
            var result = new ResultModel<LoginResultModel>();

            //检测手机验证码
            if (!await CheckMobileCode(result, model))
                return result;

            //检测账户
            var account = await _accountRepository.GetByUserName(model.UserName, model.AccountType);
            var checkAccountResult = CheckAccount(account);
            if (!checkAccountResult.Successful)
                return result.Failed(checkAccountResult.Msg);

            //检测密码
            //if (!CheckPassword(result, model, account))
            //    return result;

            using var uow = _dbContext.NewUnitOfWork();

            //判断是否激活，如果未激活需要修改为已激活状态
            if (account.Status == AccountStatus.Inactive)
            {
                if (!await _accountRepository.UpdateAccountStatus(account.Id, AccountStatus.Enabled, uow))
                {
                    return result.Failed();
                }
            }

            //更新登录信息
            var loginInfo = await UpdateLoginInfo(account, model, uow);
            if (loginInfo != null)
            {
                uow.Commit();

                if (_systemConfig.Login.VerifyCode)
                {
                    //删除验证码缓存
                    await _cacheHandler.RemoveAsync(string.Format(CacheKeys.VerifyMobileCodeKey, model.VerifyCode.Id));
                }

                //清除账户的认证信息缓存
                await _cacheHandler.RemoveAsync(string.Format(CacheKeys.AccountAuthInfo, account.Id, model.Platform.ToInt()));

                return result.Success(new LoginResultModel
                {
                    Account = account,
                    AuthInfo = loginInfo
                });
            }

            return result.Failed();
        }

        /// <summary>
        /// 检测验证码
        /// </summary>
        private async Task<bool> CheckVerifyCode(ResultModel<LoginResultModel> result, LoginModel model)
        {
            if (_systemConfig.Login.VerifyCode)
            {
                if (model.VerifyCode == null || model.VerifyCode.Code.IsNull())
                {
                    result.Failed("请输入验证码");
                    return false;
                }

                if (model.VerifyCode.Id.IsNull())
                {
                    result.Failed("验证码不存在");
                    return false;
                }

                var cacheCode = await _cacheHandler.GetAsync(string.Format(CacheKeys.VerifyCodeKey, model.VerifyCode.Id));
                if (cacheCode.IsNull())
                {
                    result.Failed("验证码不存在");
                    return false;
                }

                if (!cacheCode.Equals(model.VerifyCode.Code))
                {
                    result.Failed("验证码有误");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 检测手机验证码
        /// </summary>
        private async Task<bool> CheckMobileCode(ResultModel<LoginResultModel> result, LoginModel model)
        {
            if (model.VerifyCode == null || model.VerifyCode.Code.IsNull())
            {
                result.Failed("请输入验证码");
                return false;
            }

            if (model.VerifyCode.Id.IsNull())
            {
                result.Failed("验证码不存在");
                return false;
            }

            var cacheCode = await _cacheHandler.GetAsync(string.Format(CacheKeys.VerifyMobileCodeKey, model.VerifyCode.Id));
            if (cacheCode.IsNull())
            {
                result.Failed("验证码不存在");
                return false;
            }

            if (!cacheCode.Equals(model.VerifyCode.Code))
            {
                result.Failed("验证码有误");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 检测账户
        /// </summary>
        private IResultModel CheckAccount(AccountEntity account)
        {
            if (account == null || account.Deleted)
            {
                return ResultModel.Failed("账户不存在");
            }

            if (account.Status == AccountStatus.Closed)
            {
                return ResultModel.Failed("该账户已注销，请联系管理员");
            }

            if (account.Status == AccountStatus.Disabled)
            {
                return ResultModel.Failed("该账户已禁用，请联系管理员");
            }

            return ResultModel.Success();
        }

        /// <summary>
        /// 检测密码
        /// </summary>
        private bool CheckPassword(ResultModel<LoginResultModel> result, LoginModel model, AccountEntity account)
        {
            var password = _passwordHandler.Encrypt(account.UserName, model.Password);
            if (!account.Password.Equals(password))
            {
                result.Failed("密码错误");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 更新登录信息
        /// </summary>
        private async Task<AccountAuthInfoEntity> UpdateLoginInfo(AccountEntity account, LoginModel model, IUnitOfWork uow)
        {
            //修改登录信息(这里硬编码登录方式为JWT，暂时不想去判断是哪种方式了~)
            var authInfo = new AccountAuthInfoEntity
            {
                AccountId = account.Id,
                Platform = model.Platform,
                LoginTime = DateTime.Now.ToTimestamp(),
                LoginIP = _loginInfo.IPv4,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpiredTime = DateTime.Now.AddDays(7)//默认刷新令牌有效期7天
            };

            //设置过期时间
            if (_options != null && _options.RefreshTokenExpires > 0)
            {
                authInfo.RefreshTokenExpiredTime = DateTime.Now.AddDays(_options.RefreshTokenExpires);
            }

            Task<bool> task;
            var entity = await _authInfoRepository.Get(account.Id, model.Platform);
            if (entity != null)
            {
                authInfo.Id = entity.Id;

                task = _authInfoRepository.UpdateAsync(authInfo, uow);
            }
            else
            {
                task = _authInfoRepository.AddAsync(authInfo, uow);
            }

            return await task ? authInfo : null;
        }

        /// <summary>
        /// 生成刷新令牌
        /// </summary>
        /// <returns></returns>
        private string GenerateRefreshToken()
        {
            //生成唯一刷新令牌
            //var randomNumber = new byte[32];
            //using var rng = RandomNumberGenerator.Create();
            //rng.GetBytes(randomNumber);
            //return Convert.ToBase64String(randomNumber);
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        #endregion

        #region ==刷新令牌==

        public async Task<ResultModel<LoginResultModel>> RefreshToken(string refreshToken)
        {
            var result = new ResultModel<LoginResultModel>();
            var cacheKey = string.Format(CacheKeys.RefreshToken, refreshToken);
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
            var checkAccountResult = CheckAccount(account);
            if (!checkAccountResult.Successful)
                return result.Failed(checkAccountResult.Msg);

            return result.Success(new LoginResultModel
            {
                Account = account,
                AuthInfo = authInfo
            });
        }

        #endregion

        #region ==获取认证信息==

        public async Task<IResultModel> GetAuthInfo()
        {
            var account = await _accountRepository.GetAsync(_loginInfo.AccountId);
            var result = CheckAccount(account);
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

        #endregion

        #region ==获取认证信息==

        public async Task<AccountAuthInfoEntity> GetAuthInfo(Guid accountId, Platform platform)
        {
            if (!_cacheHandler.TryGetValue(string.Format(CacheKeys.AccountAuthInfo, accountId, platform.ToInt()), out AccountAuthInfoEntity authInfo))
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
