using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Common.Domain.Dict;
using Nm.Module.Common.Domain.Dict.Models;

namespace Nm.Module.Common.Infrastructure.Repositories.SqlServer
{
    public class DictRepository : RepositoryAbstract<DictEntity>, IDictRepository
    {
        public DictRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<DictEntity>> Query(DictQueryModel model)
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
