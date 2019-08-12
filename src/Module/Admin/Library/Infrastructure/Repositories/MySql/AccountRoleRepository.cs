using Tm.Lib.Data.Abstractions;

namespace Tm.Module.Admin.Infrastructure.Repositories.MySql
{
    public class AccountRoleRepository : SqlServer.AccountRoleRepository
    {
        public AccountRoleRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
