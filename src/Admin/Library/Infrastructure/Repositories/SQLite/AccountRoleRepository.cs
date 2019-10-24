using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Admin.Infrastructure.Repositories.SQLite
{
    public class AccountRoleRepository : SqlServer.AccountRoleRepository
    {
        public AccountRoleRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
