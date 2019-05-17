using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Blog.Infrastructure.Repositories.SQLite
{
    public class TagRepository : SqlServer.TagRepository
    {
        public TagRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
