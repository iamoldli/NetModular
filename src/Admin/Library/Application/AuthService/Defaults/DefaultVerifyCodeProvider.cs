using System;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Abstractions.LoginModels;
using NetModular.Lib.Auth.Abstractions.Providers;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Lib.Utils.Core.Helpers;
using NetModular.Module.Admin.Infrastructure;
using VerifyCodeModel = NetModular.Lib.Auth.Abstractions.Providers.VerifyCodeModel;

namespace NetModular.Module.Admin.Application.AuthService.Defaults
{
    /// <summary>
    /// 默认验证码提供器
    /// </summary>
    [Singleton]
    internal class DefaultVerifyCodeProvider : IVerifyCodeProvider
    {
        private readonly DrawingHelper _drawingHelper;
        private readonly ICacheHandler _cacheHandler;
        private readonly IConfigProvider _configProvider;
        public DefaultVerifyCodeProvider(DrawingHelper drawingHelper, ICacheHandler cacheHandler, IConfigProvider configProvider)
        {
            _drawingHelper = drawingHelper;
            _cacheHandler = cacheHandler;
            _configProvider = configProvider;
        }

        public VerifyCodeModel Create(int len = 6)
        {
            var model = new VerifyCodeModel
            {
                Id = Guid.NewGuid(),
                Base64String = _drawingHelper.DrawVerifyCodeBase64String(out string code, len)
            };

            var key = CacheKeys.AUTH_VERIFY_CODE + model.Id;

            //把验证码放到缓存中，有效期10分钟
            _cacheHandler.SetAsync(key, code, 10);

            return model;
        }

        public IResultModel Check(LoginModel model)
        {
            var config = _configProvider.Get<AuthConfig>();
            //启用验证码
            if (config.VerifyCode)
            {
                if (model.VerifyCode == null || model.VerifyCode.Code.IsNull())
                    return ResultModel.Failed("请输入验证码");

                if (model.VerifyCode.Id.IsNull())
                    return ResultModel.Failed("验证码不存在");

                var cacheCode = _cacheHandler.GetAsync(CacheKeys.AUTH_VERIFY_CODE + model.VerifyCode.Id).Result;
                if (cacheCode.IsNull())
                    return ResultModel.Failed("验证码不存在");

                if (!cacheCode.Equals(model.VerifyCode.Code))
                    return ResultModel.Failed("验证码有误");
            }

            return ResultModel.Success();
        }
    }
}
