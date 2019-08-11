using Nm.Lib.Data.Abstractions;

namespace Nm.Module.PersonnelFiles.Infrastructure.Repositories.SQLite
{
    public class UserWorkHistoryRepository : SqlServer.UserWorkHistoryRepository
    {
        public UserWorkHistoryRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
