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
    public class MenuRepository : RepositoryAbstract<MenuEntity>, IMenuRepository
    {
        public MenuRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public Task<IList<MenuEntity>> Query(Paging paging, string name = null, string code = null, Guid? parentId = null)
        {
            var query = Db.Find().LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id);

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

        public Task<MenuEntity> GetAsync(Guid id)
        {
            var query = Db.Find().LeftJoin<MenuEntity>((x, y) => x.ParentId == y.Id)
                .LeftJoin<AccountEntity>((x, y, z) => x.CreatedBy == z.Id)
                .Where((x, y, z) => x.Id == id)
                .Select((m, n, o) => new { m, ParentName = n.Name, });

            return query.FirstAsync<MenuEntity>();
        }

        public Task<IList<MenuEntity>> GetByAccount(Guid accountId)
        {
            return Db.Find().InnerJoin<RoleMenuEntity>((x, y) => x.Id == y.MenuId)
                 .InnerJoin<AccountRoleEntity>((x, y, z) => y.RoleId == z.RoleId && z.AccountId == accountId)
                 .Select((x, y, z) => new { x })
                 .ToListAsync();
        }
    }
}
