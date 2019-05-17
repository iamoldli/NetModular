using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Blog.Infrastructure.Repositories.MySql
{
    public class CategoryRepository : SqlServer.CategoryRepository
    {
        public CategoryRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}