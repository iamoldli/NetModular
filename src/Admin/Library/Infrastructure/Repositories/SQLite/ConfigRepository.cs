using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Admin.Infrastructure.Repositories.SQLite
{
    public class ConfigRepository : SqlServer.ConfigRepository
    {
        public ConfigRepository(IDbContext context) : base(context)
        {
        }
    }
}
