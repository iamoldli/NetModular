using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Lib.Data.Query;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Module.Admin.Domain.Account;
using Tm.Module.CodeGenerator.Domain.Project;
using Tm.Module.CodeGenerator.Domain.Project.Models;

namespace Tm.Module.CodeGenerator.Infrastructure.Repositories.SqlServer
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
