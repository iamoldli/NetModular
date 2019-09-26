using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Admin.Infrastructure.Repositories.MySql
{
    public class AccountConfigRepository : SqlServer.AccountConfigRepository
    {
        public AccountConfigRepository(IDbContext context) : base(context)
        {
        }
    }
}
