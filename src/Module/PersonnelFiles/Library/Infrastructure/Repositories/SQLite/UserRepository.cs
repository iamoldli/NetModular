using Tm.Lib.Data.Abstractions;

namespace Tm.Module.PersonnelFiles.Infrastructure.Repositories.SQLite
{
    public class UserRepository : SqlServer.UserRepository
    {
        public UserRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
