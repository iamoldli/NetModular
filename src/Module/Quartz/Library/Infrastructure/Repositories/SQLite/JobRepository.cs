using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Quartz.Infrastructure.Repositories.SQLite
{
    public class JobRepository : SqlServer.JobRepository
    {
        public JobRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
