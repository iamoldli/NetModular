using Nm.Lib.Data.Abstractions;

namespace Nm.Module.PersonnelFiles.Infrastructure.Repositories.SQLite
{
    public class UserRepository : SqlServer.UserRepository
    {
        public UserRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
