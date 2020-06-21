using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions.LoginModels;

namespace NetModular.Lib.Auth.Abstractions.LoginHandlers
{
    /// <summary>
    /// 用户名或邮箱登录处理器
    /// </summary>
    public interface IUserNameOrEmailLoginHandler
    {
        /// <summary>
        /// 登录处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<LoginResultModel> Handle(UserNameOrEmailLoginModel model);
    }
}
