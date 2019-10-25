using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SQLite
{
    public class PermissionRepository : SqlServer.PermissionRepository
    {
        public PermissionRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
