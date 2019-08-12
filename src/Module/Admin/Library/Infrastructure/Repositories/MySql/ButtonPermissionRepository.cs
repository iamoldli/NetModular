using Tm.Lib.Data.Abstractions;

namespace Tm.Module.Admin.Infrastructure.Repositories.MySql
{
    public class ButtonPermissionRepository : SqlServer.ButtonPermissionRepository
    {
        public ButtonPermissionRepository(IDbContext context) : base(context)
        {
        }
    }
}
