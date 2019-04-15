using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.MySql
{
    public class ButtonPermissionRepository : SqlServer.ButtonPermissionRepository
    {
        public ButtonPermissionRepository(IDbContext context) : base(context)
        {
        }
    }
}
