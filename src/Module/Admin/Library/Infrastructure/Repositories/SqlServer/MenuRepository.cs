using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Admin.Domain.AccountRole;
using Nm.Module.Admin.Domain.Menu;
using Nm.Module.Admin.Domain.Menu.Models;
using Nm.Module.Admin.Domain.RoleMenu;

namespace Nm.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class MenuRepository : RepositoryAbstract<MenuEntity>, IMenuRepository
    {
        public MenuRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IList<MenuEntity>> Query(MenuQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find();
            query.WhereIf(model.Name.NotNull(), m => m.Name.Contains(model.Name));
            query.WhereIf(model.RouteName.NotNull(), m => m.RouteName.Contains(model.RouteName));

            if (model.ParentId == null)
                model.ParentId = Guid.Empty;
            query.Where(m => m.ParentId == model.ParentId);

            if (!paging.OrderBy.Any())
            {
                query.OrderBy(m => m.Sort);
            }

            var list = await query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .Select((x, y) => new { x, CreatorName = y.Name })
                .PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<IList<MenuEntity>> QueryChildren(Guid parentId)
        {
            return Db.Find(m => m.ParentId == parentId).OrderBy(m => m.Sort).ToListAsync();
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
