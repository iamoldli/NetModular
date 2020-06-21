using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions.LoginModels;

namespace NetModular.Lib.Auth.Abstractions.LoginHandlers
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
        Task<LoginResultModel> Handle(PhoneLoginModel model);
    }
}
