using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Lib.Data.Query;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Module.Admin.Domain.Account;
using Tm.Module.CodeGenerator.Domain.Enum;
using Tm.Module.CodeGenerator.Domain.Enum.Models;

namespace Tm.Module.CodeGenerator.Infrastructure.Repositories.SqlServer
{
    public class EnumRepository : RepositoryAbstract<EnumEntity>, IEnumRepository
    {
        public EnumRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<EnumEntity>> Query(EnumQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find().WhereIf(model.Name.NotNull(), m => m.Name.Contains(model.Name));
            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            var list = await query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .Select((x, y) => new { x, Creator = y.Name })
                .PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<bool> Exists(EnumEntity entity)
        {
            return Db.Find(m =>m.Name.Equals(entity.Name))
                .WhereIf(entity.Id.NotEmpty(), m => m.Id != entity.Id)
                .ExistsAsync();
        }
    }
}
