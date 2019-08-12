using Tm.Lib.Data.Abstractions;

namespace Tm.Module.PersonnelFiles.Infrastructure.Repositories.MySql
{
    public class UserContactRepository : SqlServer.UserContactRepository
    {
        public UserContactRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}