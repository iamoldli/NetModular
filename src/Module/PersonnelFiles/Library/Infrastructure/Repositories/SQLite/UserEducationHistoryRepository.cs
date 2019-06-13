using Nm.Lib.Data.Abstractions;

namespace Nm.Module.PersonnelFiles.Infrastructure.Repositories.SQLite
{
    public class UserEducationHistoryRepository : SqlServer.UserEducationHistoryRepository
    {
        public UserEducationHistoryRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
