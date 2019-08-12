using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Lib.Data.Query;
using Tm.Module.Quartz.Domain.JobLog;
using Tm.Module.Quartz.Domain.JobLog.Models;

namespace Tm.Module.Quartz.Infrastructure.Repositories.SqlServer
{
    public class JobLogRepository : RepositoryAbstract<JobLogEntity>, IJobLogRepository
    {
        public JobLogRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<JobLogEntity>> Query(JobLogQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find();
            query.Where(m => m.JobId == model.JobId);
            query.WhereIf(model.StartDate != null, m => m.CreatedTime >= model.StartDate.Value);
            query.WhereIf(model.EndDate != null, m => m.CreatedTime < model.EndDate.Value.AddDays(1));

            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            var result = await query.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;

            return result;
        }
    }
}
