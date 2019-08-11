using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Common.Infrastructure.Repositories.MySql
{
    public class AreaRepository : SqlServer.AreaRepository
    {
        public AreaRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}