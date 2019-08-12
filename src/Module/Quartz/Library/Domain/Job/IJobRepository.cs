using System.Collections.Generic;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Module.Quartz.Domain.Job.Models;

namespace Tm.Module.Quartz.Domain.Job
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

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Exists(JobEntity entity);

        /// <summary>
        /// 根据任务组判断是否存在
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        Task<bool> ExistsByGroup(string group);

        /// <summary>
        /// 修改任务状态
        /// </summary>
        /// <param name="jobKey"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<bool> UpdateStatus(string jobKey, JobStatus status);
    }
}
