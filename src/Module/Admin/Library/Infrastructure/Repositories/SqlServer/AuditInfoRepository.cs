using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Pagination;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.AuditInfo;
using NetModular.Module.Admin.Domain.ModuleInfo;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class AuditInfoRepository : RepositoryAbstract<AuditInfoEntity>, IAuditInfoRepository
    {
        public AuditInfoRepository(IDbContext context) : base(context)
        {
        }

        public Task<IList<AuditInfoEntity>> Query(Paging paging, string moduleCode, string controller, string action, DateTime? startTime,
            DateTime? endTime)
        {
            var query = Db.Find();
            query.WhereIf(moduleCode.NotNull(), m => m.Area == moduleCode);
            query.WhereIf(controller.NotNull(), m => m.Controller == controller);
            query.WhereIf(action.NotNull(), m => m.Action == action);
            query.WhereIf(startTime != null, m => m.ExecutionTime >= startTime);
            query.WhereIf(endTime != null, m => m.ExecutionTime <= endTime);

            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }
            return query.LeftJoin<AccountEntity>((x, y) => x.AccountId == y.Id)
                .LeftJoin<ModuleInfoEntity>((x, y, z) => x.Area == z.Code)
                .Select((x, y, z) => new
                {
                    x.Id,
                    x.AccountId,
                    x.Area,
                    x.Controller,
                    x.Action,
                    x.ExecutionTime,
                    x.ExecutionDuration,
                    x.IP,
                    x.Platform,
                    Creator = y.Name,
                    ModuleName = z.Name
                })
                .PaginationAsync(paging);
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
