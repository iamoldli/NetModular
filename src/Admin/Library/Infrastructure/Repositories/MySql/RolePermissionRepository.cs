using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.MySql
{
    public class RolePermissionRepository : SqlServer.RolePermissionRepository
    {
        public RolePermissionRepository(IDbContext context) : base(context)
        {
        }
    }
}
