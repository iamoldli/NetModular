using Tm.Lib.Data.Abstractions;

namespace Tm.Module.Quartz.Infrastructure.Repositories.MySql
{
    public class JobRepository : SqlServer.JobRepository
    {
        public JobRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}