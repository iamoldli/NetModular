using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.CodeGenerator.Domain.Project;
using Nm.Module.CodeGenerator.Domain.Project.Models;

namespace Nm.Module.CodeGenerator.Infrastructure.Repositories.SqlServer
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
