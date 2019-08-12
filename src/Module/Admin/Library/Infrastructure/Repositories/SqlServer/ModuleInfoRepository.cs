using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Lib.Data.Query;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Module.Admin.Domain.Account;
using Tm.Module.Admin.Domain.ModuleInfo;
using Tm.Module.Admin.Domain.ModuleInfo.Models;

namespace Tm.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class ModuleInfoRepository : RepositoryAbstract<ModuleInfoEntity>, IModuleInfoRepository
    {
        public ModuleInfoRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<ModuleInfoEntity>> Query(ModuleInfoQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find();
            query.WhereIf(model.Name.NotNull(), m => m.Name.Contains(model.Name));
            query.WhereIf(model.Code.NotNull(), m => m.Code.Contains(model.Code));

            if (!paging.OrderBy.Any())
                query.OrderByDescending(m => m.Id);

            var list = await query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .Select((x, y) => new { x, Creator = y.Name })
                .PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<bool> Exists(ModuleInfoEntity entity,IDbTransaction transaction)
        {
            var query = Db.Find(m => m.Code == entity.Code);
            query.WhereIf(entity.Id.NotEmpty(), m => m.Id != entity.Id);
            query.UseTran(transaction);
            return query.ExistsAsync();
        }

        public Task<bool> UpdateByCode(ModuleInfoEntity entity)
        {
            return Db.Find().Where(m => m.Code == entity.Code).UpdateAsync(m => new ModuleInfoEntity
            {
                Name = entity.Name,
                Version = entity.Version,
                Remarks = entity.Remarks
            });
        }
    }
}
