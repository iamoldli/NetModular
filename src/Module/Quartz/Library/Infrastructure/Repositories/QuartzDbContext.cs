using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;

namespace Tm.Module.Quartz.Infrastructure.Repositories
{
    public class QuartzDbContext : DbContext
    {
        public QuartzDbContext(IDbContextOptions options) : base(options)
        {
        }
    }
}
