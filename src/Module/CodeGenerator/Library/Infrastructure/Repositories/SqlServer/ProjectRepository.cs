using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
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

            var query = Db.Find();
            query.WhereNotNull(model.Name, m => m.Name.Contains(model.Name));
            query.WhereNotNull(model.Code, m => m.Code.Contains(model.Code));

            var joinQuery = query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id);
            if (!paging.OrderBy.Any())
            {
                joinQuery.OrderByDescending((x, y) => x.Id);
            }

            joinQuery.Select((x, y) => new { x, Creator = y.Name });

            var result = await joinQuery.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;

            return result;
        }
    }
}
