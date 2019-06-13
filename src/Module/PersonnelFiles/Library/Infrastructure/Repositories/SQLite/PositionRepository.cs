using Nm.Lib.Data.Abstractions;

namespace Nm.Module.PersonnelFiles.Infrastructure.Repositories.SQLite
{
    public class PositionRepository : SqlServer.PositionRepository
    {
        public PositionRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
