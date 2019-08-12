using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;

namespace Tm.Module.PersonnelFiles.Infrastructure.Repositories
{
    public class PersonnelFilesDbContext : DbContext
    {
        public PersonnelFilesDbContext(IDbContextOptions options) : base(options)
        {
        }
    }
}
