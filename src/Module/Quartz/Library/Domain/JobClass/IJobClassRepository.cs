using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Pagination;
using Nm.Module.Quartz.Domain.JobClass.Models;

namespace Nm.Module.Quartz.Domain.JobClass
{
    /// <summary>
    /// 任务类型仓储
    /// </summary>
    public interface IJobClassRepository : IRepository<JobClassEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<JobClassEntity>> Query(JobClassQueryModel model);
    }
}
