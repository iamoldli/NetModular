using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions.LoginModels;

namespace NetModular.Lib.Auth.Abstractions
{
    /// <summary>
    /// 租户解析器接口
    /// </summary>
    public interface ITenantResolver
    {
        /// <summary>
        /// 解析租户ID
        /// </summary>
        /// <param name="loginResultModel">登录结果</param>
        /// <returns></returns>
        Task Resolve(LoginResultModel loginResultModel);
    }
}
