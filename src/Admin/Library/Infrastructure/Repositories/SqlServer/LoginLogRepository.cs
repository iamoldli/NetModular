using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Data.Query;
using NetModular.Module.Admin.Domain.LoginLog;
using NetModular.Module.Admin.Domain.LoginLog.Models;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class LoginLogRepository : RepositoryAbstract<LoginLogEntity>, ILoginLogRepository
    {
        public LoginLogRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<LoginLogEntity>> Query(LoginLogQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find();
            query.WhereNotNull(model.AccountId, m => m.AccountId == model.AccountId.Value);
            query.WhereNotNull(model.Platform, m => m.Platform == model.Platform.Value);
            query.WhereNotNull(model.LoginMode, m => m.LoginMode == model.LoginMode.Value);
            query.WhereNotNull(model.Success, m => m.Success == model.Success.Value);
            query.WhereNotNull(model.StartDate, m => m.LoginTime >= model.StartDate.Value.Date);
            query.WhereNotNull(model.EndDate, m => m.LoginTime < model.EndDate.Value.Date.AddDays(1));

            var list = await query.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }
    }
}
