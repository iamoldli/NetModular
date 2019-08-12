using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Lib.Data.Query;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Module.Admin.Domain.Account;
using Tm.Module.Quartz.Domain.Group;
using Tm.Module.Quartz.Domain.Group.Models;

namespace Tm.Module.Quartz.Infrastructure.Repositories.SqlServer
{
    public class GroupRepository : RepositoryAbstract<GroupEntity>, IGroupRepository
    {
        public GroupRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<GroupEntity>> Query(GroupQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find();
            query.WhereIf(model.Name.NotNull(), m => m.Name.Contains(model.Name));
            query.WhereIf(model.Code.NotNull(), m => m.Code.Contains(model.Code));
            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            var result = await query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .Select((x, y) => new { x, Creator = y.Name })
                .PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;

            return result;
        }

        public Task<bool> Exists(GroupEntity entity)
        {
            var query = Db.Find(m => m.Code == entity.Code);
            query.WhereIf(entity.Id.NotEmpty(), m => m.Id != entity.Id);
            return query.ExistsAsync();
        }
    }
}
