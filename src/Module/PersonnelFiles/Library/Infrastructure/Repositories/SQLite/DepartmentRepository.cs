using Nm.Lib.Data.Abstractions;

namespace Nm.Module.PersonnelFiles.Infrastructure.Repositories.SQLite
{
    public class DepartmentRepository : SqlServer.DepartmentRepository
    {
        public DepartmentRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
