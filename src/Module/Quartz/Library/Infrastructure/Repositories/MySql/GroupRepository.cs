using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Quartz.Infrastructure.Repositories.MySql
{
    public class GroupRepository : SqlServer.GroupRepository
    {
        public GroupRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}