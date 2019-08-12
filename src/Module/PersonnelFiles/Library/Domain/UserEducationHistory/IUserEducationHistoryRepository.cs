using System.Collections.Generic;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Module.PersonnelFiles.Domain.UserEducationHistory.Models;

namespace Tm.Module.PersonnelFiles.Domain.UserEducationHistory
{
    /// <summary>
    /// 用户教育经历仓储
    /// </summary>
    public interface IUserEducationHistoryRepository : IRepository<UserEducationHistoryEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<UserEducationHistoryEntity>> Query(UserEducationHistoryQueryModel model);
    }
}
