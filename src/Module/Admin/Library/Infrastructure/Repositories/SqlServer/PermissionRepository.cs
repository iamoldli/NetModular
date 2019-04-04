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
using NetModular.Module.Admin.Domain.ButtonPermission;
using NetModular.Module.Admin.Domain.MenuPermission;
using NetModular.Module.Admin.Domain.ModuleInfo;
using NetModular.Module.Admin.Domain.Permission;
using NetModular.Module.Admin.Domain.RoleMenu;
using NetModular.Module.Admin.Domain.RoleMenuButton;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class PermissionRepository : RepositoryAbstract<Permission>, IPermissionRepository
    {
        public PermissionRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> Exists(Permission entity)
        {
            var query = Db.Find(m => m.ModuleCode.Equals(entity.ModuleCode) && m.Controller.Equals(entity.Controller) && m.Action.Equals(entity.Action));

            query.WhereIf(entity.Id != Guid.Empty, m => m.Id != entity.Id);
            return query.ExistsAsync();
        }

        public Task<bool> Exists(Guid id)
        {
            return ExistsAsync(m => m.Id.Equals(id));
        }

        public Task<bool> ExistsWidthModule(string moduleCode)
        {
            return ExistsAsync(m => m.ModuleCode.Equals(moduleCode));
        }

        public Task<IList<Permission>> Query(Paging paging, string moduleCode, string name, string controller, string action)
        {
            var query = Db.Find();

            query.WhereIf(moduleCode.NotNull(), m => m.ModuleCode.Equals(moduleCode));
            query.WhereIf(name.NotNull(), m => m.Name.Contains(name));
            query.WhereIf(controller.NotNull(), m => m.Controller.Equals(controller));
            query.WhereIf(action.NotNull(), m => m.Action.Equals(action));

            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            return query.LeftJoin<ModuleInfo>((x, y) => x.ModuleCode == y.Code)
                 .LeftJoin<Account>((x, y, z) => x.CreatedBy.Equals(z.Id))
                 .Select((x, y, z) => new { x, ModuleName = y.Name, Creator = z.Name }).PaginationAsync(paging);
        }

        public Task<IList<Permission>> QueryByMenu(Guid menuId)
        {
            return Db.Find().InnerJoin<MenuPermission>((p, m) => p.Id == m.PermissionId).Where((p, m) => m.MenuId == menuId)
                .ToListAsync();
        }

        public Task<IList<Permission>> QueryByButton(Guid buttonId)
        {
            return Db.Find().InnerJoin<ButtonPermission>((p, m) => p.Id == m.PermissionId).Where((p, m) => m.ButtonId == buttonId)
                .ToListAsync();
        }

        public async Task<IList<Permission>> QueryByAccount(Guid accountId)
        {
            var list = new List<Permission>();
            var menuPermissionListTask = Db.Find().InnerJoin<MenuPermission>((x, y) => x.Id == y.PermissionId)
                .InnerJoin<RoleMenu>((x, y, z) => y.MenuId == z.MenuId)
                .InnerJoin<AccountRole>((x, y, z, m) => z.RoleId == m.RoleId && m.AccountId == accountId)
                .ToListAsync();

            var btnPermissionListTask = Db.Find().InnerJoin<ButtonPermission>((x, y) => x.Id == y.PermissionId)
                .InnerJoin<RoleMenuButton>((x, y, z) => y.ButtonId == z.ButtonId)
                .InnerJoin<AccountRole>((x, y, z, m) => z.RoleId == m.RoleId && m.AccountId == accountId)
                .ToListAsync();

            var menuPermissionList = await menuPermissionListTask;
            var btnPermissionList = await btnPermissionListTask;

            foreach (var p in menuPermissionList)
            {
                if (list.All(m => m.Id != p.Id))
                {
                    list.Add(p);
                }
            }

            foreach (var p in btnPermissionList)
            {
                if (list.All(m => m.Id != p.Id))
                {
                    list.Add(p);
                }
            }

            return list;
        }
    }
}
