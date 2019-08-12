using Tm.Lib.Data.Abstractions;

namespace Tm.Module.Admin.Infrastructure.Repositories.SQLite
{
    public class MenuRepository : SqlServer.MenuRepository
    {
        public MenuRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
