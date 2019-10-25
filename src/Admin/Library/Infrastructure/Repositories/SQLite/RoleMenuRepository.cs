using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SQLite
{
    public class RoleMenuRepository : SqlServer.RoleMenuRepository
    {
        public RoleMenuRepository(IDbContext context) : base(context)
        {
        }
    }
}
