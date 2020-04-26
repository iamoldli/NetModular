using System.Collections.Generic;
using System.Linq;
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
            query.WhereNotNull(model.StartDate, m => m.LoginTime >= model.StartDate.Value.Date);
            query.WhereNotNull(model.EndDate, m => m.LoginTime < model.EndDate.Value.Date.AddDays(1));

            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            //导出全部
            if (model.IsExport && model.Export.Mode == ExportMode.All)
            {
                model.ExportCount = await query.CountAsync();
                if (model.IsOutOfExportCountLimit)
                {
                    return new List<LoginLogEntity>();
                }
                return await query.ToListAsync();
            }

            var list = await query.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }
    }
}
