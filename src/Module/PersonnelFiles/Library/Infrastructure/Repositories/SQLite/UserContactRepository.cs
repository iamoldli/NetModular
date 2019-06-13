using Nm.Lib.Data.Abstractions;

namespace Nm.Module.PersonnelFiles.Infrastructure.Repositories.SQLite
{
    public class UserContactRepository : SqlServer.UserContactRepository
    {
        public UserContactRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
