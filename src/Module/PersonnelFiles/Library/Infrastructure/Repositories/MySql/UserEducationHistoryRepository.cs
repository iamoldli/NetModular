using Tm.Lib.Data.Abstractions;

namespace Tm.Module.PersonnelFiles.Infrastructure.Repositories.MySql
{
    public class UserEducationHistoryRepository : SqlServer.UserEducationHistoryRepository
    {
        public UserEducationHistoryRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}