using Tm.Lib.Data.Abstractions;

namespace Tm.Module.PersonnelFiles.Infrastructure.Repositories.SQLite
{
    public class UserContactRepository : SqlServer.UserContactRepository
    {
        public UserContactRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
