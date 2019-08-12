using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;

namespace Tm.Module.Admin.Infrastructure.Repositories
{
    public class AdminDbContext : DbContext
    {
        public AdminDbContext(IDbContextOptions options) : base(options)
        {
        }
    }
}
