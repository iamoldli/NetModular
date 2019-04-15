using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.MySql
{
    public class AuditInfoRepository : SqlServer.AuditInfoRepository
    {
        public AuditInfoRepository(IDbContext context) : base(context)
        {
        }
    }
}
