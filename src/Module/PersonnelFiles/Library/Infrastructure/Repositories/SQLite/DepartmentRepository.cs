using Tm.Lib.Data.Abstractions;

namespace Tm.Module.PersonnelFiles.Infrastructure.Repositories.SQLite
{
    public class DepartmentRepository : SqlServer.DepartmentRepository
    {
        public DepartmentRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
