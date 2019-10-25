using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Data.Query;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.AuditInfo;
using NetModular.Module.Admin.Domain.AuditInfo.Models;
using NetModular.Module.Admin.Domain.ModuleInfo;
using NetModular.Module.Admin.Infrastructure.Repositories.SqlServer.Sql;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
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
            query.WhereNotNull(model.ModuleCode, m => m.Area == model.ModuleCode);
            query.WhereNotNull(model.Controller, m => m.Controller == model.Controller);
            query.WhereNotNull(model.Action, m => m.Action == model.Action);
            query.WhereNotNull(model.StartTime, m => m.ExecutionTime >= model.StartTime);
            query.WhereNotNull(model.EndTime, m => m.ExecutionTime <= model.EndTime);

            var joinQuery = query.LeftJoin<AccountEntity>((x, y) => x.AccountId == y.Id)
                .LeftJoin<ModuleInfoEntity>((x, y, z) => x.Area == z.Code);

            if (!paging.OrderBy.Any())
            {
                joinQuery.OrderByDescending((x, y, z) => x.Id);
            }

            joinQuery.Select((x, y, z) => new
            {
                x,
                Creator = y.Name,
                ModuleName = z.Name
            });

            var list = await joinQuery.PaginationAsync(paging);

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

        public virtual Task<IEnumerable<ChatDataRow>> QueryLatestWeekPv()
        {
            var sql = string.Format(AuditInfoSql.QueryLatestWeekPv, Db.EntityDescriptor.TableName);
            return Db.QueryAsync<ChatDataRow>(sql);
        }
    }
}
