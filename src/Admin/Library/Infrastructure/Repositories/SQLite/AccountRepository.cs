using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Admin.Infrastructure.Repositories.SQLite
{
    public class AccountRepository : SqlServer.AccountRepository
    {
        public AccountRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
