using System.Threading.Tasks;

namespace NetModular.Module.Admin.Application.AuthService.Interfaces
{
    /// <summary>
    /// 手机验证码提供器
    /// </summary>
    public interface IPhoneVerifyCodeProvider
    {
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="areaCode">区号</param>
        /// <returns></returns>
        Task<IResultModel> Send(string phone, string areaCode = "086");

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="code">验证码</param>
        /// <param name="areaCode">区号</param>
        /// <returns></returns>
        Task<IResultModel> Verify(string phone, string code, string areaCode = "086");
    }
}
