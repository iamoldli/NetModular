using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Blog.Domain.Category;
using Nm.Module.Blog.Domain.Category.Models;

namespace Nm.Module.Blog.Infrastructure.Repositories.SqlServer
{
    public class CategoryRepository : RepositoryAbstract<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<CategoryEntity>> Query(CategoryQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find();

            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            var result = await query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .Select((x, y) => new { x, Creator = y.Name })
                .PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;

            return result;
        }
    }
}
