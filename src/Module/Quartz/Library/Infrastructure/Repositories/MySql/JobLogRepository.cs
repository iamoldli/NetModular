using Tm.Lib.Data.Abstractions;

namespace Tm.Module.Quartz.Infrastructure.Repositories.MySql
{
    public class JobLogRepository : SqlServer.JobLogRepository
    {
        public JobLogRepository(IDbContext context) : base(context)
        {
        }
    }
}
