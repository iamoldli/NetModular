using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.PersonnelFiles.Domain.UserEducationHistory;
using Nm.Module.PersonnelFiles.Domain.UserEducationHistory.Models;

namespace Nm.Module.PersonnelFiles.Infrastructure.Repositories.SqlServer
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
