using Tm.Lib.Data.Abstractions;

namespace Tm.Module.Common.Infrastructure.Repositories.MySql
{
    public class AreaRepository : SqlServer.AreaRepository
    {
        public AreaRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}