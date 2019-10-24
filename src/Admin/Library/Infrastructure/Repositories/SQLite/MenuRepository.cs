using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Admin.Infrastructure.Repositories.SQLite
{
    public class MenuRepository : SqlServer.MenuRepository
    {
        public MenuRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
