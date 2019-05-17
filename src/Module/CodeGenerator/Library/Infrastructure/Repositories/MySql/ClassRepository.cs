using Nm.Lib.Data.Abstractions;

namespace Nm.Module.CodeGenerator.Infrastructure.Repositories.MySql
{
    public class ClassRepository : SqlServer.ClassRepository
    {
        public ClassRepository(IDbContext context) : base(context)
        {
        }
    }
}
