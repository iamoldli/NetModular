using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.CodeGenerator.Domain.Enum;
using Nm.Module.CodeGenerator.Domain.Enum.Models;

namespace Nm.Module.CodeGenerator.Infrastructure.Repositories.SqlServer
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
