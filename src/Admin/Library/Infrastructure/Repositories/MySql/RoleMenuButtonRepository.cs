using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Admin.Infrastructure.Repositories.MySql
{
    public class RoleMenuButtonRepository : SqlServer.RoleMenuButtonRepository
    {
        public RoleMenuButtonRepository(IDbContext context) : base(context)
        {
        }
    }
}
