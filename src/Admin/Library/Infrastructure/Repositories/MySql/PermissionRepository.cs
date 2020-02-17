using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.MySql
{
    public class PermissionRepository : SqlServer.PermissionRepository
    {
        public PermissionRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
