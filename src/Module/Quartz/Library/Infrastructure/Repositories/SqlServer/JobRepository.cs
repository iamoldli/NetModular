using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Quartz.Domain.Job;
using Nm.Module.Quartz.Domain.Job.Models;

namespace Nm.Module.Quartz.Infrastructure.Repositories.SqlServer
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
            query.WhereNotNull(model.Name, m => m.Name.Contains(model.Name));

            var joinQuery = query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id);
            if (!paging.OrderBy.Any())
            {
                joinQuery.OrderByDescending((x, y) => x.Id);
            }

            joinQuery.Select((x, y) => new { x, Creator = y.Name });

            var result = await joinQuery.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;

            return result;
        }

        public Task<bool> Exists(JobEntity entity)
        {
            var query = Db.Find(m => m.Code == entity.Code && m.Group == entity.Group);
            query.WhereNotEmpty(entity.Id, m => m.Id != entity.Id);

            return query.ExistsAsync();
        }

        public Task<bool> ExistsByGroup(string group)
        {
            return Db.Find(m => m.Group == group).ExistsAsync();
        }

        public Task<bool> UpdateStatus(string jobKey, JobStatus status)
        {
            return Db.Find(m => m.JobKey == jobKey).UpdateAsync(m => new JobEntity { Status = status }, false);
        }
    }
}
