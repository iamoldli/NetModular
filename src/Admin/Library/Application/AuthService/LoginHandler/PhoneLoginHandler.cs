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

namespace NetModular.Module.Admin.Application.AuthService.LoginHandler
{
    /// <summary>
    /// 手机号登录处理器
    /// </summary>
    [Singleton]
    public class PhoneLoginHandler : LoginHandlerAbstract
    {
        private readonly IAccountRepository _repository;
        private readonly IPhoneVerifyCodeProvider _phoneVerifyCodeProvider;

        public PhoneLoginHandler(IVerifyCodeProvider verifyCodeProvider, IConfigProvider configProvider, IAccountAuthInfoRepository authInfoRepository, IAccountRepository repository, ICacheHandler cacheHandler, ILoginLogHandler logHandler, ILogger<UserNameLoginHandler> logger, IPhoneVerifyCodeProvider phoneVerifyCodeProvider) : base(verifyCodeProvider, configProvider, authInfoRepository, cacheHandler, logHandler, logger)
        {
            _repository = repository;
            _phoneVerifyCodeProvider = phoneVerifyCodeProvider;
        }

        /// <summary>
        /// 登录处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResultModel<LoginResultModel>> Handle(PhoneLoginModel model)
        {
            var log = CreateLog(model);
            log.LoginMode = Domain.LoginLog.LoginMode.Phone;
            log.Phone = model.Phone;

            var result = await Handle(model, log);
            await SaveLog(log, result);

            return result;
        }

        /// <summary>
        /// 登录处理
        /// </summary>
        private async Task<ResultModel<LoginResultModel>> Handle(PhoneLoginModel model, LoginLogEntity log)
        {
            var result = new ResultModel<LoginResultModel>();
            var config = _configProvider.Get<AuthConfig>();
            if (!config.LoginMode.Phone)
                return result.Failed("不允许使用手机号登录的方式");

            //检测图片验证码
            var verifyCodeCheckResult = _verifyCodeProvider.Check(model);
            if (!verifyCodeCheckResult.Successful)
                return result.Failed(verifyCodeCheckResult.Msg);

            //检测手机验证码
            var verifyResult = await _phoneVerifyCodeProvider.Verify(model.Phone, model.Code, model.AreaCode);
            if (!verifyResult.Successful)
                return result.Failed(verifyResult.Msg);

            //查询账户
            var account = await _repository.GetByPhone(model.Phone, model.AccountType);
            if (account == null)
                return result.Failed("账户信息不存在");

            log.AccountId = account.Id;

            //检测账户
            var accountCheckResult = account.Check();
            if (!accountCheckResult.Successful)
                return result.Failed(accountCheckResult.Msg);

            //更新认证信息并返回登录结果
            var resultModel = await UpdateAuthInfo(account, model, config);
            return resultModel != null ? result.Success(resultModel) : result.Failed();
        }
    }
}
