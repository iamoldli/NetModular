using Nm.Lib.Data.Abstractions;

namespace Nm.Module.CodeGenerator.Infrastructure.Repositories.MySql
{
    public class EnumRepository : SqlServer.EnumRepository
    {
        public EnumRepository(IDbContext context) : base(context)
        {
        }
    }
}
