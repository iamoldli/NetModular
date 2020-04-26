using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Admin.Domain.LoginLog.Models;

namespace NetModular.Module.Admin.Domain.LoginLog
{
    /// <summary>
    /// 登录日志仓储
    /// </summary>
    public interface ILoginLogRepository : IRepository<LoginLogEntity>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<LoginLogEntity>> Query(LoginLogQueryModel model);
    }
}
