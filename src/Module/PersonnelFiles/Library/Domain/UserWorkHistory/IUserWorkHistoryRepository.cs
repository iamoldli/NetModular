using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Module.PersonnelFiles.Domain.UserWorkHistory.Models;

namespace Nm.Module.PersonnelFiles.Domain.UserWorkHistory
{
    /// <summary>
    /// 用户工作经历仓储
    /// </summary>
    public interface IUserWorkHistoryRepository : IRepository<UserWorkHistoryEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<UserWorkHistoryEntity>> Query(UserWorkHistoryQueryModel model);
    }
}
