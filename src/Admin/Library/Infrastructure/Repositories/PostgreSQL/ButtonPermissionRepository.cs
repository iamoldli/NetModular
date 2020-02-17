using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.PostgreSQL
{
    public class ButtonPermissionRepository : SqlServer.ButtonPermissionRepository
    {
        public ButtonPermissionRepository(IDbContext context) : base(context)
        {
        }
    }
}
