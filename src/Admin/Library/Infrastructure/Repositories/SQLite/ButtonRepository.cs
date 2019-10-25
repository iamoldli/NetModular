using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SQLite
{
    public class ButtonRepository : SqlServer.ButtonRepository
    {
        public ButtonRepository(IDbContext context) : base(context)
        {
        }
    }
}
