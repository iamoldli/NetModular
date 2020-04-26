using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Module.Admin.Application.AuthService.Interfaces;
using NetModular.Module.Admin.Application.AuthService.ResultModels;
using NetModular.Module.Admin.Application.AuthService.ViewModels;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.AccountAuthInfo;
using NetModular.Module.Admin.Domain.LoginLog;
using NetModular.Module.Admin.Infrastructure.PasswordHandler;

namespace NetModular.Module.Admin.Application.AuthService.LoginHandler
{
    /// <summary>
    /// 用户名或邮箱登录
    /// </summary>
    [Singleton]
    public class UserNameOrEmailLoginHandler : LoginHandlerAbstract
    {
        private readonly IPasswordHandler _passwordHandler;
        private readonly IAccountRepository _repository;

        public UserNameOrEmailLoginHandler(IVerifyCodeProvider verifyCodeProvider, IConfigProvider configProvider,
            IAccountAuthInfoRepository authInfoRepository, IAccountRepository repository,
            IPasswordHandler passwordHandler, ICacheHandler cacheHandler, ILoginLogHandler logHandler,
            ILogger<UserNameLoginHandler> logger) : base(verifyCodeProvider, configProvider, authInfoRepository,
            cacheHandler, logHandler, logger)
        {
            _repository = repository;
            _passwordHandler = passwordHandler;
        }

        /// <summary>
        /// 登录处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResultModel<LoginResultModel>> Handle(UserNameOrEmailLoginModel model)
        {
            var log = CreateLog(model);
            log.LoginMode = Domain.LoginLog.LoginMode.UserNameOrEmail;
            log.UserName = model.UserNameOrEmail;

            var result = await Handle(model, log);
            await SaveLog(log, result);

            return result;
        }

        /// <summary>
        /// 登录处理
        /// </summary>
        private async Task<ResultModel<LoginResultModel>> Handle(UserNameOrEmailLoginModel model, LoginLogEntity log)
        {
            var result = new ResultModel<LoginResultModel>();
            var config = _configProvider.Get<AuthConfig>();
            if (!config.LoginMode.UserNameOrEmail)
                return result.Failed("不允许使用用户名或邮箱的登录方式");

            //检测验证码
            var verifyCodeCheckResult = _verifyCodeProvider.Check(model);
            if (!verifyCodeCheckResult.Successful)
                return result.Failed(verifyCodeCheckResult.Msg);

            //查询账户
            var account = await _repository.GetByUserNameOrEmail(model.UserNameOrEmail, model.AccountType);
            if (account == null)
                return result.Failed("账户信息不存在");

            log.AccountId = account.Id;

            //检测账户
            var accountCheckResult = account.Check();
            if (!accountCheckResult.Successful)
                return result.Failed(accountCheckResult.Msg);

            //检测密码
            var password = _passwordHandler.Encrypt(account.UserName, model.Password);
            if (!account.Password.Equals(password))
                return result.Failed("密码错误");

            //更新认证信息并返回登录结果
            var resultModel = await UpdateAuthInfo(account, model, config);
            return resultModel != null ? result.Success(resultModel) : result.Failed();
        }
    }
}
