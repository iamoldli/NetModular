using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions.LoginModels;

namespace NetModular.Lib.Auth.Abstractions.LoginHandlers
{
    /// <summary>
    /// 邮件登录处理
    /// </summary>
    public interface IEmailLoginHandler
    {
        /// <summary>
        /// 登录处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<LoginResultModel> Handle(EmailLoginModel model);
    }
}
