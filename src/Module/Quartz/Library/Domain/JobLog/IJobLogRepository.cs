using System.Collections.Generic;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Module.Quartz.Domain.JobLog.Models;

namespace Tm.Module.Quartz.Domain.JobLog
{
    public interface IJobLogRepository : IRepository<JobLogEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<JobLogEntity>> Query(JobLogQueryModel model);
    }
}
