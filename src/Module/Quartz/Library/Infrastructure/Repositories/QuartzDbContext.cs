using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;

namespace Nm.Module.Quartz.Infrastructure.Repositories
{
    public class QuartzDbContext : DbContext
    {
        public QuartzDbContext(IDbContextOptions options) : base(options)
        {
        }
    }
}
