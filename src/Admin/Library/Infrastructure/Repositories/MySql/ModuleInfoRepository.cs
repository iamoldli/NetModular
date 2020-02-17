using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.MySql
{
    public class ModuleInfoRepository : SqlServer.ModuleInfoRepository
    {
        public ModuleInfoRepository(IDbContext context) : base(context)
        {
        }
    }
}
