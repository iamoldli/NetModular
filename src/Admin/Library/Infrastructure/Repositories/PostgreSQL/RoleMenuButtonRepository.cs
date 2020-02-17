using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.PostgreSQL
{
    public class RoleMenuButtonRepository : SqlServer.RoleMenuButtonRepository
    {
        public RoleMenuButtonRepository(IDbContext context) : base(context)
        {
        }
    }
}
