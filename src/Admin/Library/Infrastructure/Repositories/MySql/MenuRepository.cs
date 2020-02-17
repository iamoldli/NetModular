using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.MySql
{
    public class MenuRepository : SqlServer.MenuRepository
    {
        public MenuRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
