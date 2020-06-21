using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Abstractions.LoginModels;
using NetModular.Lib.Auth.Abstractions.Providers;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Config.Abstractions;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.AccountAuthInfo;
using NetModular.Module.Admin.Domain.LoginLog;
using NetModular.Module.Admin.Infrastructure;

namespace NetModular.Module.Admin.Application.AuthService.LoginHandler
{
    public abstract class LoginHandlerAbstract
    {
        protected readonly ILogger<LoginHandlerAbstract> _logger;
        protected readonly IVerifyCodeProvider _verifyCodeProvider;
        protected readonly IConfigProvider _configProvider;
        protected readonly IAccountAuthInfoRepository _authInfoRepository;
        protected readonly ICacheHandler _cacheHandler;
        protected readonly ILoginLogProvider _logHandler;
        private readonly ITenantResolver _tenantResolver;

        protected LoginHandlerAbstract(ILogger<LoginHandlerAbstract> logger, IVerifyCodeProvider verifyCodeProvider, IConfigProvider configProvider, IAccountAuthInfoRepository authInfoRepository, ICacheHandler cacheHandler, ILoginLogProvider logHandler, ITenantResolver tenantResolver)
        {
            _logger = logger;
            _verifyCodeProvider = verifyCodeProvider;
            _configProvider = configProvider;
            _authInfoRepository = authInfoRepository;
            _cacheHandler = cacheHandler;
            _logHandler = logHandler;
            _tenantResolver = tenantResolver;
        }

        /// <summary>
        /// 保存日志
        /// </summary>
        protected async ValueTask SaveLog(LoginResultModel result)
        {
            //保存日志，不能抛出异常以免影响登录本身的功能
            try
            {
                await _logHandler.Handle(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("登录日志存储失败：{@ex}", ex);
            }
        }

        /// <summary>
        /// 解析租户编号
        /// </summary>
        /// <param name="resultModel"></param>
        /// <returns></returns>
        protected Task ResolveTenant(LoginResultModel resultModel)
        {
            //解析租户编号
            return _tenantResolver.Resolve(resultModel);
        }

        /// <summary>
        /// 更新账户认证信息
        /// </summary>
        protected async Task UpdateAuthInfo(LoginResultModel resultModel, LoginModel loginModel)
        {
            resultModel.RefreshToken = GenerateRefreshToken();

            var authInfo = new AccountAuthInfoEntity
            {
                AccountId = resultModel.AccountId,
                Platform = resultModel.Platform,
                LoginTime = resultModel.LoginTime.ToTimestamp(),
                RefreshToken = resultModel.RefreshToken,
                RefreshTokenExpiredTime = DateTime.Now.AddDays(7)//默认刷新令牌有效期7天
            };


            var config = _configProvider.Get<AuthConfig>();

            //设置过期时间
            if (config.Jwt.RefreshTokenExpires > 0)
            {
                authInfo.RefreshTokenExpiredTime = DateTime.Now.AddDays(config.Jwt.RefreshTokenExpires);
            }

            Task<bool> task;
            var entity = await _authInfoRepository.Get(resultModel.AccountId, resultModel.Platform);
            if (entity != null)
            {
                authInfo.Id = entity.Id;
                task = _authInfoRepository.UpdateAsync(authInfo);
            }
            else
            {
                task = _authInfoRepository.AddAsync(authInfo);
            }

            if (await task)
            {
                //判断是否开启验证码功能，删除验证码缓存
                if (config.VerifyCode)
                {
                    await _cacheHandler.RemoveAsync(CacheKeys.AUTH_VERIFY_CODE + loginModel.VerifyCode.Id);
                }

                //清除账户的认证信息缓存
                await _cacheHandler.RemoveAsync($"{CacheKeys.ACCOUNT_AUTH_INFO}{resultModel.AccountId}:{resultModel.Platform.ToInt()}");
            }
        }

        /// <summary>
        /// 生成刷新令牌
        /// </summary>
        /// <returns></returns>
        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
