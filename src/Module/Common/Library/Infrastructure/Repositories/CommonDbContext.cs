using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;

namespace Tm.Module.Common.Infrastructure.Repositories
{
    public class CommonDbContext : DbContext
    {
        public CommonDbContext(IDbContextOptions options) : base(options)
        {
        }
    }
}
