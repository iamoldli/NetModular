using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Quartz.Infrastructure.Repositories.MySql
{
    public class JobLogRepository : SqlServer.JobLogRepository
    {
        public JobLogRepository(IDbContext context) : base(context)
        {
        }
    }
}
