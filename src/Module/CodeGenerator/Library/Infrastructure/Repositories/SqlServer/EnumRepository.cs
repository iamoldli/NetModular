using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
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
            var query = Db.Find().WhereNotNull(model.Name, m => m.Name.Contains(model.Name));

            var joinQuery = query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id);
            if (!paging.OrderBy.Any())
            {
                joinQuery.OrderByDescending((x, y) => x.Id);
            }

            joinQuery.Select((x, y) => new { x, Creator = y.Name });

            var list = await joinQuery.PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<bool> Exists(EnumEntity entity)
        {
            return Db.Find(m => m.Name.Equals(entity.Name))
                .WhereNotEmpty(entity.Id, m => m.Id != entity.Id)
                .ExistsAsync();
        }
    }
}
