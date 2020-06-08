using System;
using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Module.Admin.Application.AuthService.ResultModels;
using NetModular.Module.Admin.Application.AuthService.ViewModels;

namespace NetModular.Module.Admin.Application.AuthService.LoginHandler
{
    [Singleton]
    public class DefaultCustomLoginHandler : ICustomLoginHandler
    {
        public Task<ResultModel<LoginResultModel>> Handle(CustomLoginModel model)
        {
            throw new NotImplementedException();
        }
    }
}
