using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.MySql
{
    public class MenuPermissionRepository : SqlServer.MenuPermissionRepository
    {
        public MenuPermissionRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
