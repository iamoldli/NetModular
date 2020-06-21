using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Admin.Domain.Tenant;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class TenantRepository : RepositoryAbstract<TenantEntity>, ITenantRepository
    {
        public TenantRepository(IDbContext context) : base(context)
        {
        }
    }
}
