using Tm.Lib.Data.Abstractions;

namespace Tm.Module.CodeGenerator.Infrastructure.Repositories.MySql
{
    public class EnumItemRepository : SqlServer.EnumItemRepository
    {
        public EnumItemRepository(IDbContext context) : base(context)
        {
        }
    }
}
