using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Lib.Data.Query;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Module.PersonnelFiles.Domain.UserWorkHistory;
using Tm.Module.PersonnelFiles.Domain.UserWorkHistory.Models;

namespace Tm.Module.PersonnelFiles.Infrastructure.Repositories.SqlServer
{
    public class UserWorkHistoryRepository : RepositoryAbstract<UserWorkHistoryEntity>, IUserWorkHistoryRepository
    {
        public UserWorkHistoryRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<UserWorkHistoryEntity>> Query(UserWorkHistoryQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find(m=>m.UserId==model.UserId);
            query.WhereIf(model.EnterpriseName.NotNull(), m => m.EnterpriseName.Contains(model.EnterpriseName));

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
