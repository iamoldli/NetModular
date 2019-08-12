using Tm.Lib.Data.Abstractions;

namespace Tm.Module.CodeGenerator.Infrastructure.Repositories.SQLite
{
    public class ProjectRepository : SqlServer.ProjectRepository
    {
        public ProjectRepository(IDbContext context) : base(context)
        {
        }
    }
}
