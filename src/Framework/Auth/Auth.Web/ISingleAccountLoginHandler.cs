using System.Threading.Tasks;

namespace NetModular.Lib.Auth.Web
{
    /// <summary>
    /// 单账户登录处理器
    /// </summary>
    public interface ISingleAccountLoginHandler
    {
        /// <summary>
        /// 验证账户是否已在别处登录
        /// </summary>
        /// <returns></returns>
        Task<bool> Validate();
    }
}
