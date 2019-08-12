using Tm.Lib.Data.Abstractions;

namespace Tm.Module.PersonnelFiles.Infrastructure.Repositories.SQLite
{
    public class UserEducationHistoryRepository : SqlServer.UserEducationHistoryRepository
    {
        public UserEducationHistoryRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
