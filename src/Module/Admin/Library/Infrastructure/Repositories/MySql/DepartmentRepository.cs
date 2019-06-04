using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Admin.Infrastructure.Repositories.MySql
{
    public class DepartmentRepository : SqlServer.DepartmentRepository
    {
        public DepartmentRepository(IDbContext context) : base(context)
        {
        }
    }
}
