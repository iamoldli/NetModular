using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;

namespace Tm.Module.CodeGenerator.Infrastructure.Repositories
{
    public class CodeGeneratorDbContext : DbContext
    {
        public CodeGeneratorDbContext(IDbContextOptions options) : base(options)
        {
        }
    }
}
