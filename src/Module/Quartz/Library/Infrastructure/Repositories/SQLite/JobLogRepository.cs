using Tm.Lib.Data.Abstractions;

namespace Tm.Module.Quartz.Infrastructure.Repositories.SQLite
{
    public class JobLogRepository : SqlServer.JobLogRepository
    {
        public JobLogRepository(IDbContext context) : base(context)
        {
        }
    }
}
