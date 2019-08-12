using Tm.Lib.Data.Abstractions;

namespace Tm.Module.Quartz.Infrastructure.Repositories.MySql
{
    public class GroupRepository : SqlServer.GroupRepository
    {
        public GroupRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}