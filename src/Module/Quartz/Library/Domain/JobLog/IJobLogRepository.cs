using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Module.Quartz.Domain.JobLog.Models;

namespace Nm.Module.Quartz.Domain.JobLog
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
