using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Config.Abstractions;
using NetModular.Module.Admin.Application.AuthService.Interfaces;
using NetModular.Module.Admin.Application.AuthService.ResultModels;
using NetModular.Module.Admin.Application.AuthService.ViewModels;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.AccountAuthInfo;
using NetModular.Module.Admin.Domain.LoginLog;
using NetModular.Module.Admin.Infrastructure;

namespace NetModular.Module.Admin.Application.AuthService.LoginHandler
{
    public abstract class LoginHandlerAbstract
    {
        protected readonly IVerifyCodeProvider _verifyCodeProvider;
        protected readonly IConfigProvider _configProvider;
        protected readonly IAccountAuthInfoRepository _authInfoRepository;
        protected readonly ICacheHandler _cacheHandler;
        protected readonly ILoginLogHandler _logHandler;
        protected readonly ILogger _logger;

        protected LoginHandlerAbstract(IVerifyCodeProvider verifyCodeProvider, IConfigProvider configProvider, IAccountAuthInfoRepository authInfoRepository, ICacheHandler cacheHandler, ILoginLogHandler logHandler, ILogger logger)
        {
            _verifyCodeProvider = verifyCodeProvider;
            _configProvider = configProvider;
            _authInfoRepository = authInfoRepository;
            _cacheHandler = cacheHandler;
            _logHandler = logHandler;
            _logger = logger;
        }

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected LoginLogEntity CreateLog(LoginModel model)
        {
            var config = _configProvider.Get<AdminConfig>();
            //不启用登录日志
            if (!config.LoginLog)
                return null;

            return new LoginLogEntity
            {
                LoginTime = DateTime.Now,
                IP = model.IP,
                Platform = model.Platform,
                UserAgent = model.UserAgent
            };
        }

        /// <summary>
        /// 保存日志
        /// </summary>
        protected async ValueTask SaveLog(LoginLogEntity log, ResultModel<LoginResultModel> result)
        {
            log.Success = result.Successful;
            log.Error = result.Msg;

            //保存日志，不能抛出异常以免影响登录本身的功能
            try
            {
                await _logHandler.Handle(log);
            }
            catch (Exception ex)
            {
                _logger.LogError("登录日志存储失败：{@ex}", ex);
            }
        }

        /// <summary>
        /// 更新账户认证信息
        /// </summary>
        protected async Task<LoginResultModel> UpdateAuthInfo(AccountEntity account, LoginModel model, AuthConfig config)
        {
            var authInfo = new AccountAuthInfoEntity
            {
                AccountId = account.Id,
                Platform = model.Platform,
                LoginTime = DateTime.Now.ToTimestamp(),
                LoginIP = model.IP,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpiredTime = DateTime.Now.AddDays(7)//默认刷新令牌有效期7天
            };

            //设置过期时间
            if (config.Jwt.RefreshTokenExpires > 0)
            {
                authInfo.RefreshTokenExpiredTime = DateTime.Now.AddDays(config.Jwt.RefreshTokenExpires);
            }

            Task<bool> task;
            var entity = await _authInfoRepository.Get(account.Id, model.Platform);
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
                    await _cacheHandler.RemoveAsync($"{CacheKeys.AUTH_VERIFY_CODE}:{model.VerifyCode.Id}");
                }

                //清除账户的认证信息缓存
                await _cacheHandler.RemoveAsync($"{CacheKeys.ACCOUNT_AUTH_INFO}:{account.Id}:{model.Platform.ToInt()}");

                return new LoginResultModel
                {
                    Account = account,
                    AuthInfo = authInfo
                };
            }

            return null;
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
