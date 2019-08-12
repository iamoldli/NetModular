using Tm.Lib.Data.Abstractions;

namespace Tm.Module.CodeGenerator.Infrastructure.Repositories.MySql
{
    public class PropertyRepository : SqlServer.PropertyRepository
    {
        public PropertyRepository(IDbContext context) : base(context)
        {
        }
    }
}
