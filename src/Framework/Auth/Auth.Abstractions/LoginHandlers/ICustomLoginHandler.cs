using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions.LoginModels;

namespace NetModular.Lib.Auth.Abstractions.LoginHandlers
{
    /// <summary>
    /// 自定义登录处理器
    /// </summary>
    public interface ICustomLoginHandler
    {
        /// <summary>
        /// 登录处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<LoginResultModel> Handle(CustomLoginModel model);
    }
}
