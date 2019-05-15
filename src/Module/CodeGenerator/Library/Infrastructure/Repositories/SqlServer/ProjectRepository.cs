using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Data.Query;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.CodeGenerator.Domain.Project;
using NetModular.Module.CodeGenerator.Domain.Project.Models;

namespace NetModular.Module.CodeGenerator.Infrastructure.Repositories.SqlServer
{
    public class ProjectRepository : RepositoryAbstract<ProjectEntity>, IProjectRepository
    {
        public ProjectRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<ProjectEntity>> Query(ProjectQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find(m => m.Deleted == false);
            query.WhereIf(model.Name.NotNull(), m => m.Name.Contains(model.Name));
            query.WhereIf(model.Code.NotNull(), m => m.Code.Contains(model.Code));

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
