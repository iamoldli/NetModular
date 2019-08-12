using Tm.Lib.Data.Abstractions;

namespace Tm.Module.PersonnelFiles.Infrastructure.Repositories.SQLite
{
    public class PositionRepository : SqlServer.PositionRepository
    {
        public PositionRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
