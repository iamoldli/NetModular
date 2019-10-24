using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Admin.Infrastructure.Repositories.SQLite
{
    public class ButtonRepository : SqlServer.ButtonRepository
    {
        public ButtonRepository(IDbContext context) : base(context)
        {
        }
    }
}
