using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Module.PersonnelFiles.Domain.UserWorkHistory;
using Nm.Module.PersonnelFiles.Domain.UserWorkHistory.Models;

namespace Nm.Module.PersonnelFiles.Infrastructure.Repositories.SqlServer
{
    public class UserWorkHistoryRepository : RepositoryAbstract<UserWorkHistoryEntity>, IUserWorkHistoryRepository
    {
        public UserWorkHistoryRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<UserWorkHistoryEntity>> Query(UserWorkHistoryQueryModel model)
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
