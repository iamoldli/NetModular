using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SQLite
{
    public class RoleRepository : SqlServer.RoleRepository
    {
        public RoleRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
