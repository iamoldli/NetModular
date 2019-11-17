using Data.Common.Domain;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;

namespace Data.Common.Repository
{
    public class CategoryRepository : RepositoryAbstract<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(IDbContext context) : base(context)
        {
        }
    }
}
