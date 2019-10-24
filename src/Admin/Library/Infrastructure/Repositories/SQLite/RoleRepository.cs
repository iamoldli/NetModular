using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Admin.Infrastructure.Repositories.SQLite
{
    public class RoleRepository : SqlServer.RoleRepository
    {
        public RoleRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
