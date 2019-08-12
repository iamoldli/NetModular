using Tm.Lib.Data.Abstractions;

namespace Tm.Module.PersonnelFiles.Infrastructure.Repositories.SQLite
{
    public class UserWorkHistoryRepository : SqlServer.UserWorkHistoryRepository
    {
        public UserWorkHistoryRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
