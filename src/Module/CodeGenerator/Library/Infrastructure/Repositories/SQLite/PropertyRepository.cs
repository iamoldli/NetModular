using Nm.Lib.Data.Abstractions;

namespace Nm.Module.CodeGenerator.Infrastructure.Repositories.SQLite
{
    public class PropertyRepository : SqlServer.PropertyRepository
    {
        public PropertyRepository(IDbContext context) : base(context)
        {
        }
    }
}
