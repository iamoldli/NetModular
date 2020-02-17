using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.MySql
{
    public class AccountAuthInfoRepository : SqlServer.AccountAuthInfoRepository
    {
        public AccountAuthInfoRepository(IDbContext context) : base(context)
        {
        }
    }
}
