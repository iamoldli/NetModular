using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Pagination;
using Nm.Module.Quartz.Domain.Group.Models;

namespace Nm.Module.Quartz.Domain.Group
{
    /// <summary>
    /// 任务组仓储
    /// </summary>
    public interface IGroupRepository : IRepository<GroupEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<GroupEntity>> Query(GroupQueryModel model);
    }
}
