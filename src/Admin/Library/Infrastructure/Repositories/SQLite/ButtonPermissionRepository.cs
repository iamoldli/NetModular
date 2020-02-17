using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SQLite
{
    public class ButtonPermissionRepository : SqlServer.ButtonPermissionRepository
    {
        public ButtonPermissionRepository(IDbContext context) : base(context)
        {
        }
    }
}
