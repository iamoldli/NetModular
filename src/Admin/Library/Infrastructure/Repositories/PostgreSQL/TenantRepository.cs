using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.PostgreSQL
{
    public class TenantRepository : SqlServer.TenantRepository
    {
        public TenantRepository(IDbContext context) : base(context)
        {
        }
    }
}
