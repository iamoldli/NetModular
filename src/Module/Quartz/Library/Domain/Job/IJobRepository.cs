using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Pagination;
using Nm.Module.Quartz.Domain.Job.Models;

namespace Nm.Module.Quartz.Domain.Job
{
    /// <summary>
    /// 任务仓储
    /// </summary>
    public interface IJobRepository : IRepository<JobEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<JobEntity>> Query(JobQueryModel model);
    }
}
