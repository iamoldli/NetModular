using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Admin.Infrastructure.Repositories.MySql
{
    public class PermissionRepository : SqlServer.PermissionRepository
    {
        public PermissionRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
