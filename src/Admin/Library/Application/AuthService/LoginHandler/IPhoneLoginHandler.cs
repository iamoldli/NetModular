using System.Threading.Tasks;
using NetModular.Module.Admin.Application.AuthService.ResultModels;
using NetModular.Module.Admin.Application.AuthService.ViewModels;

namespace NetModular.Module.Admin.Application.AuthService.LoginHandler
{
    /// <summary>
    /// 手机登录处理
    /// </summary>
    public interface IPhoneLoginHandler
    {
        /// <summary>
        /// 登录处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ResultModel<LoginResultModel>> Handle(PhoneLoginModel model);
    }
}
