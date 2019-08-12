using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Lib.Data.Query;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Module.Admin.Domain.Account;
using Tm.Module.Quartz.Domain.Job;
using Tm.Module.Quartz.Domain.Job.Models;

namespace Tm.Module.Quartz.Infrastructure.Repositories.SqlServer
{
    public class JobRepository : RepositoryAbstract<JobEntity>, IJobRepository
    {
        public JobRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<JobEntity>> Query(JobQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find();
            query.WhereIf(model.Name.NotNull(), m => m.Name.Contains(model.Name));

            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            var result = await query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .Select((x, y) => new { x, Creator = y.Name })
                .PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;

            return result;
        }

        public Task<bool> Exists(JobEntity entity)
        {
            var query = Db.Find(m => m.Code == entity.Code && m.Group == entity.Group);
            query.WhereIf(entity.Id.NotEmpty(), m => m.Id != entity.Id);

            return query.ExistsAsync();
        }

        public Task<bool> ExistsByGroup(string group)
        {
            return Db.Find(m => m.Group == group).ExistsAsync();
        }

        public Task<bool> UpdateStatus(string jobKey, JobStatus status)
        {
            return Db.Find(m => m.JobKey == jobKey).UpdateAsync(m => new JobEntity {Status = status}, false);
        }
    }
}
