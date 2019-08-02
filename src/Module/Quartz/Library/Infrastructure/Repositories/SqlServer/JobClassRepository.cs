using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Module.Quartz.Domain.JobClass;
using Nm.Module.Quartz.Domain.JobClass.Models;

namespace Nm.Module.Quartz.Infrastructure.Repositories.SqlServer
{
    public class JobClassRepository : RepositoryAbstract<JobClassEntity>, IJobClassRepository
    {
        public JobClassRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<JobClassEntity>> Query(JobClassQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find();

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
