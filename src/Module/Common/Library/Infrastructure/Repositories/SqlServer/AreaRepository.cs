using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Module.Common.Domain.Area;
using Nm.Module.Common.Domain.Area.Models;

namespace Nm.Module.Common.Infrastructure.Repositories.SqlServer
{
    public class AreaRepository : RepositoryAbstract<AreaEntity>, IAreaRepository
    {
        public AreaRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<AreaEntity>> Query(AreaQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find();

            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            var result = await query.PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;

            return result;
        }
    }
}
