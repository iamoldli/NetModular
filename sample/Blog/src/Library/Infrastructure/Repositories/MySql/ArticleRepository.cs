using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Blog.Infrastructure.Repositories.MySql
{
    public class ArticleRepository : SqlServer.ArticleRepository
    {
        public ArticleRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}