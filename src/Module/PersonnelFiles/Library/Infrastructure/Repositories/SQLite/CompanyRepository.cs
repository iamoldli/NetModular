using Tm.Lib.Data.Abstractions;

namespace Tm.Module.PersonnelFiles.Infrastructure.Repositories.SQLite
{
    public class CompanyRepository : SqlServer.CompanyRepository
    {
        public CompanyRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
