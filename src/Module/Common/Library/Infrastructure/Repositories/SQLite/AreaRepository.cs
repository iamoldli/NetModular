using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Common.Infrastructure.Repositories.SQLite
{
    public class AreaRepository : SqlServer.AreaRepository
    {
        public AreaRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
