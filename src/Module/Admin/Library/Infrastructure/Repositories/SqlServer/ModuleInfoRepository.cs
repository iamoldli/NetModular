using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Admin.Domain.ModuleInfo;
using Nm.Module.Admin.Domain.ModuleInfo.Models;

namespace Nm.Module.Admin.Infrastructure.Repositories.SqlServer
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
            query.WhereNotNull(model.Name, m => m.Name.Contains(model.Name));
            query.WhereNotNull(model.Code, m => m.Code.Contains(model.Code));

            var joinQuery = query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id);

            if (!paging.OrderBy.Any())
                joinQuery.OrderByDescending((x, y) => x.Id);

            joinQuery.Select((x, y) => new { x, Creator = y.Name });
            var list = await joinQuery.PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<bool> Exists(ModuleInfoEntity entity, IDbTransaction transaction)
        {
            var query = Db.Find(m => m.Code == entity.Code);
            query.WhereNotEmpty(entity.Id, m => m.Id != entity.Id);
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
