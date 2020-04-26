using System.Threading.Tasks;
using NetModular.Module.Admin.Domain.LoginLog;

namespace NetModular.Module.Admin.Application.AuthService.Interfaces
{
    /// <summary>
    /// 登录日志处理
    /// </summary>
    public interface ILoginLogHandler
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Handle(LoginLogEntity entity);
    }
}
