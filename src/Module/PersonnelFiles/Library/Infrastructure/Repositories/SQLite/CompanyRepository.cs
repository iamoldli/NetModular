using Nm.Lib.Data.Abstractions;

namespace Nm.Module.PersonnelFiles.Infrastructure.Repositories.SQLite
{
    public class CompanyRepository : SqlServer.CompanyRepository
    {
        public CompanyRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
