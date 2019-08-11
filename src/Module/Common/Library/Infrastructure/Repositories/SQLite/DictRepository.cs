using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Common.Infrastructure.Repositories.SQLite
{
    public class DictRepository : SqlServer.DictRepository
    {
        public DictRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
