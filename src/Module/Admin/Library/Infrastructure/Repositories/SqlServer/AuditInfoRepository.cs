using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Admin.Domain.AuditInfo;
using Nm.Module.Admin.Domain.AuditInfo.Models;
using Nm.Module.Admin.Domain.ModuleInfo;

namespace Nm.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class AuditInfoRepository : RepositoryAbstract<AuditInfoEntity>, IAuditInfoRepository
    {
        public AuditInfoRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<AuditInfoEntity>> Query(AuditInfoQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find();
            query.WhereIf(model.ModuleCode.NotNull(), m => m.Area == model.ModuleCode);
            query.WhereIf(model.Controller.NotNull(), m => m.Controller == model.Controller);
            query.WhereIf(model.Action.NotNull(), m => m.Action == model.Action);
            query.WhereIf(model.StartTime != null, m => m.ExecutionTime >= model.StartTime);
            query.WhereIf(model.EndTime != null, m => m.ExecutionTime <= model.EndTime);

            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }
            var list = await query.LeftJoin<AccountEntity>((x, y) => x.AccountId == y.Id)
                .LeftJoin<ModuleInfoEntity>((x, y, z) => x.Area == z.Code)
                .Select((x, y, z) => new
                {
                    x,
                    Creator = y.Name,
                    ModuleName = z.Name
                })
                .PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<AuditInfoEntity> Details(int id)
        {
            return Db.Find(m => m.Id == id)
                .LeftJoin<AccountEntity>((x, y) => x.AccountId == y.Id)
                .LeftJoin<ModuleInfoEntity>((x, y, z) => x.Area == z.Code)
                .Select((x, y, z) => new
                {
                    x,
                    Creator = y.Name,
                    ModuleName = z.Name
                }).FirstAsync<AuditInfoEntity>();
        }
    }
}
