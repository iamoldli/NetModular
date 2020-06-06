using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SQLite
{
    public class RoleButtonRepository : SqlServer.RoleButtonRepository
    {
        public RoleButtonRepository(IDbContext context) : base(context)
        {
        }
    }
}
