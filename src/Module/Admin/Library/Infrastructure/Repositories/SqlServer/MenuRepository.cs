using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Pagination;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.AccountRole;
using NetModular.Module.Admin.Domain.Menu;
using NetModular.Module.Admin.Domain.RoleMenu;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class MenuRepository : RepositoryAbstract<Menu>, IMenuRepository
    {
        public MenuRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public Task<IList<Menu>> Query(Paging paging, string name = null, string code = null, Guid? parentId = null)
        {
            var query = Db.Find().LeftJoin<Account>((x, y) => x.CreatedBy == y.Id);

            query.WhereIf(name.NotNull(), (x, y) => x.Name.Contains(name));
            query.WhereIf(code.NotNull(), (x, y) => x.RouteName.Contains(code));

            if (parentId == null)
                parentId = Guid.Empty;
            query.Where((x, y) => x.ParentId == parentId);

            if (!paging.OrderBy.Any())
            {
                query.OrderBy((x, y) => x.Sort);
            }

            query.Select((x, y) => new { x, CreatorName = y.Name });

            return query.PaginationAsync(paging);
        }

        public Task<bool> ExistsChild(Guid id)
        {
            return ExistsAsync(e => e.ParentId == id);
        }

        public Task<bool> ExistsNameByParentId(string name, Guid id, Guid parentId = default(Guid))
        {
            return ExistsAsync(e => e.ParentId == parentId && e.Name.Equals(name) && e.Id != id);
        }

        public Task<bool> ExistsWidthModule(string moduleCode)
        {
            return ExistsAsync(e => e.ModuleCode == moduleCode);
        }

        public Task<Menu> GetAsync(Guid id)
        {
            var query = Db.Find().LeftJoin<Menu>((x, y) => x.ParentId == y.Id)
                .LeftJoin<Account>((x, y, z) => x.CreatedBy == z.Id)
                .Where((x, y, z) => x.Id == id)
                .Select((m, n, o) => new { m, ParentName = n.Name, });

            return query.FirstAsync<Menu>();
        }

        public Task<IList<Menu>> GetByAccount(Guid accountId)
        {
            return Db.Find().InnerJoin<RoleMenu>((x, y) => x.Id == y.MenuId)
                 .InnerJoin<AccountRole>((x, y, z) => y.RoleId == z.RoleId && z.AccountId == accountId)
                 .Select((x, y, z) => new { x })
                 .ToListAsync();
        }
    }
}
