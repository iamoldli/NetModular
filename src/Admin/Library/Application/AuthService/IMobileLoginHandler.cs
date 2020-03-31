using System.Threading.Tasks;

namespace NetModular.Module.Admin.Application.AuthService
{
    /// <summary>
    /// 手机登录处理接口
    /// </summary>
    public interface IMobileLoginHandler
    {
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="areaCode">区号</param>
        /// <returns></returns>
        Task<string> GenerateCode(string mobile, string areaCode = "086");

        /// <summary>
        /// 校验验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<bool> VerifyCode(string mobile, string code);
    }
}
