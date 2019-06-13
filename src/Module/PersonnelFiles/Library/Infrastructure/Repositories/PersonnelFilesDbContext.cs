using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;

namespace Nm.Module.PersonnelFiles.Infrastructure.Repositories
{
    public class PersonnelFilesDbContext : DbContext
    {
        public PersonnelFilesDbContext(IDbContextOptions options) : base(options)
        {
        }
    }
}
