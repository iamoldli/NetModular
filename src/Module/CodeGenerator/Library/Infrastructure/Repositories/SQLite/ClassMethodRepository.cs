using Nm.Lib.Data.Abstractions;

namespace Nm.Module.CodeGenerator.Infrastructure.Repositories.SQLite
{
    public class ClassMethodRepository : SqlServer.ClassMethodRepository
    {
        public ClassMethodRepository(IDbContext context) : base(context)
        {
        }
    }
}
