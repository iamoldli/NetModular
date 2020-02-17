using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.PostgreSQL
{
    public class AccountRoleRepository : SqlServer.AccountRoleRepository
    {
        public AccountRoleRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
