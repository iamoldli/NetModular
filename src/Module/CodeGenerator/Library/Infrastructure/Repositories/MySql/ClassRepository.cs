using Tm.Lib.Data.Abstractions;

namespace Tm.Module.CodeGenerator.Infrastructure.Repositories.MySql
{
    public class ClassRepository : SqlServer.ClassRepository
    {
        public ClassRepository(IDbContext context) : base(context)
        {
        }
    }
}
