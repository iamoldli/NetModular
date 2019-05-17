using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Blog.Infrastructure.Repositories.SQLite
{
    public class ArticleRepository : SqlServer.ArticleRepository
    {
        public ArticleRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
