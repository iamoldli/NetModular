using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Lib.Data.Query;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Module.PersonnelFiles.Domain.UserEducationHistory;
using Tm.Module.PersonnelFiles.Domain.UserEducationHistory.Models;

namespace Tm.Module.PersonnelFiles.Infrastructure.Repositories.SqlServer
{
    public class UserEducationHistoryRepository : RepositoryAbstract<UserEducationHistoryEntity>, IUserEducationHistoryRepository
    {
        public UserEducationHistoryRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<UserEducationHistoryEntity>> Query(UserEducationHistoryQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find(m=>m.UserId==model.UserId);
            query.WhereIf(model.SchoolName.NotNull(), m => m.SchoolName.Contains(model.SchoolName));

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
