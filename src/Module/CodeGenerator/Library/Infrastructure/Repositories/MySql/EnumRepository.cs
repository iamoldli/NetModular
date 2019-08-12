using Tm.Lib.Data.Abstractions;

namespace Tm.Module.CodeGenerator.Infrastructure.Repositories.MySql
{
    public class EnumRepository : SqlServer.EnumRepository
    {
        public EnumRepository(IDbContext context) : base(context)
        {
        }
    }
}
