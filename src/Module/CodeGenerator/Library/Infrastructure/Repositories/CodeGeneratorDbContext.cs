using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;

namespace NetModular.Module.CodeGenerator.Infrastructure.Repositories
{
    public class CodeGeneratorDbContext : DbContext
    {
        public CodeGeneratorDbContext(IDbContextOptions options) : base(options)
        {
        }
    }
}
