using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.PostgreSQL
{
    public class AccountAuthInfoRepository : SqlServer.AccountAuthInfoRepository
    {
        public AccountAuthInfoRepository(IDbContext context) : base(context)
        {
        }
    }
}
