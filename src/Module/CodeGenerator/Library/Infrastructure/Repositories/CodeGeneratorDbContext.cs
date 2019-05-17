using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;

namespace Nm.Module.CodeGenerator.Infrastructure.Repositories
{
    public class CodeGeneratorDbContext : DbContext
    {
        public CodeGeneratorDbContext(IDbContextOptions options) : base(options)
        {
        }
    }
}
