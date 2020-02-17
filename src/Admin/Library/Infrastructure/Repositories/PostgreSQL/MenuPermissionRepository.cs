using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.PostgreSQL
{
    public class MenuPermissionRepository : SqlServer.MenuPermissionRepository
    {
        public MenuPermissionRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
