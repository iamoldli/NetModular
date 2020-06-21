using System;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions.LoginHandlers;
using NetModular.Lib.Auth.Abstractions.LoginModels;
using NetModular.Lib.Utils.Core.Attributes;

namespace NetModular.Module.Admin.Application.AuthService.LoginHandler
{
    /// <summary>
    /// 自定义登录默认实现
    /// </summary>
    [Singleton]
    public class DefaultCustomLoginHandler : ICustomLoginHandler
    {
        public Task<LoginResultModel> Handle(CustomLoginModel model)
        {
            throw new NotImplementedException();
        }
    }
}
