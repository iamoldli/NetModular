using Nm.Lib.Data.Abstractions;

namespace Nm.Module.CodeGenerator.Infrastructure.Repositories.SQLite
{
    public class EnumItemRepository : SqlServer.EnumItemRepository
    {
        public EnumItemRepository(IDbContext context) : base(context)
        {
        }
    }
}
