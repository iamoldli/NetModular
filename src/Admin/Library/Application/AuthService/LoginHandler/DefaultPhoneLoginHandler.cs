using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Abstractions.LoginHandlers;
using NetModular.Lib.Auth.Abstractions.LoginModels;
using NetModular.Lib.Auth.Abstractions.Providers;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.AccountAuthInfo;

namespace NetModular.Module.Admin.Application.AuthService.LoginHandler
{
    /// <summary>
    /// 手机号登录处理器
    /// </summary>
    [Singleton]
    public class DefaultPhoneLoginHandler : LoginHandlerAbstract, IPhoneLoginHandler
    {
        private readonly IAccountRepository _repository;
        private readonly IPhoneVerifyCodeProvider _phoneVerifyCodeProvider;

        public DefaultPhoneLoginHandler(ILogger<LoginHandlerAbstract> logger, IVerifyCodeProvider verifyCodeProvider, IConfigProvider configProvider, IAccountAuthInfoRepository authInfoRepository, ICacheHandler cacheHandler, ILoginLogProvider logHandler, ITenantResolver tenantResolver, IAccountRepository repository, IPhoneVerifyCodeProvider phoneVerifyCodeProvider) : base(logger, verifyCodeProvider, configProvider, authInfoRepository, cacheHandler, logHandler, tenantResolver)
        {
            _repository = repository;
            _phoneVerifyCodeProvider = phoneVerifyCodeProvider;
        }

        /// <summary>
        /// 登录处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<LoginResultModel> Handle(PhoneLoginModel model)
        {
            var resultModel = new LoginResultModel
            {
                LoginMode = LoginMode.Phone,
                Phone = model.Phone,
                Platform = model.Platform,
                LoginTime = DateTime.Now,
                AccountType = model.AccountType
            };

            //解析租户编号
            await ResolveTenant(resultModel);

            //检测
            var checkResult = await Check(model, resultModel);
            if (checkResult.Successful)
            {
                resultModel.Success = true;
                //更新认证信息并返回登录结果
                await UpdateAuthInfo(resultModel, model);
            }
            else
            {
                resultModel.Success = false;
                resultModel.Error = checkResult.Msg;
            }

            //记录日志
            await SaveLog(resultModel);

            return resultModel;
        }

        /// <summary>
        /// 登录处理
        /// </summary>
        private async Task<IResultModel> Check(PhoneLoginModel model, LoginResultModel resultModel)
        {
            var config = _configProvider.Get<AuthConfig>();
            if (!config.LoginMode.Phone)
                return ResultModel.Failed("不允许使用手机号登录的方式");

            //检测图片验证码
            var verifyCodeCheckResult = _verifyCodeProvider.Check(model);
            if (!verifyCodeCheckResult.Successful)
                return ResultModel.Failed(verifyCodeCheckResult.Msg);

            //检测手机验证码
            var verifyResult = await _phoneVerifyCodeProvider.Verify(resultModel.Phone, model.Code, model.AreaCode);
            if (!verifyResult.Successful)
                return ResultModel.Failed(verifyResult.Msg);

            //查询账户
            var account = await _repository.GetByPhone(model.Phone, model.AccountType, resultModel.TenantId);
            if (account == null)
                return ResultModel.Failed("账户不存在");

            //设置账户编号和名称
            resultModel.AccountId = account.Id;
            resultModel.Name = account.Name;

            //检测账户
            var accountCheckResult = account.Check();
            if (!accountCheckResult.Successful)
                return ResultModel.Failed(accountCheckResult.Msg);

            //更新认证信息并返回登录结果
            return ResultModel.Success();
        }
    }
}
