using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Quartz.Infrastructure.Repositories.SQLite
{
    public class JobClassRepository : SqlServer.JobClassRepository
    {
        public JobClassRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
