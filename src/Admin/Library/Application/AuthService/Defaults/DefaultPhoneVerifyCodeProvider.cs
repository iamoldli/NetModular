using System;
using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Module.Admin.Application.AuthService.Interfaces;

namespace NetModular.Module.Admin.Application.AuthService.Defaults
{
    [Singleton]
    internal class DefaultPhoneVerifyCodeProvider : IPhoneVerifyCodeProvider
    {
        public Task<IResultModel> Send(string phone, string areaCode = "086")
        {
            throw new NotImplementedException("手机号登录需要自己实现手机验证码逻辑");
        }

        public Task<IResultModel> Verify(string phone, string code, string areaCode = "086")
        {
            throw new NotImplementedException("手机号登录需要自己实现手机验证码逻辑");
        }
    }
}
