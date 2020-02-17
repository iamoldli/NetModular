using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.PostgreSQL
{
    public class AccountConfigRepository : SqlServer.AccountConfigRepository
    {
        public AccountConfigRepository(IDbContext context) : base(context)
        {
        }
    }
}
