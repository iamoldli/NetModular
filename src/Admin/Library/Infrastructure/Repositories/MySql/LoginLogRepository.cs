using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.MySql
{
    public class LoginLogRepository : SqlServer.LoginLogRepository
    {
        public LoginLogRepository(IDbContext context) : base(context)
        {
        }
    }
}
