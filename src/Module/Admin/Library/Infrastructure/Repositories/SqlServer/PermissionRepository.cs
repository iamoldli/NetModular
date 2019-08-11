using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Admin.Domain.AccountRole;
using Nm.Module.Admin.Domain.Button;
using Nm.Module.Admin.Domain.ButtonPermission;
using Nm.Module.Admin.Domain.Menu;
using Nm.Module.Admin.Domain.MenuPermission;
using Nm.Module.Admin.Domain.ModuleInfo;
using Nm.Module.Admin.Domain.Permission;
using Nm.Module.Admin.Domain.Permission.Models;
using Nm.Module.Admin.Domain.RoleMenu;
using Nm.Module.Admin.Domain.RoleMenuButton;

namespace Nm.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class PermissionRepository : RepositoryAbstract<PermissionEntity>, IPermissionRepository
    {
        public PermissionRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> Exists(PermissionEntity entity, IDbTransaction transaction)
        {
            var query = Db.Find(m => m.ModuleCode.Equals(entity.ModuleCode));
            query.Where(m => m.Controller.Equals(entity.Controller));
            query.Where(m => m.Action.Equals(entity.Action));
            query.Where(m => m.HttpMethod.Equals(entity.HttpMethod));

            query.WhereIf(entity.Id != Guid.Empty, m => m.Id != entity.Id);

            query.UseTran(transaction);
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

        public async Task<IList<PermissionEntity>> Query(PermissionQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find();

            query.WhereIf(model.ModuleCode.NotNull(), m => m.ModuleCode.Equals(model.ModuleCode));
            query.WhereIf(model.Name.NotNull(), m => m.Name.Contains(model.Name));
            query.WhereIf(model.Controller.NotNull(), m => m.Controller.Equals(model.Controller));
            query.WhereIf(model.Action.NotNull(), m => m.Action.Equals(model.Action));

            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            var list = await query.LeftJoin<ModuleInfoEntity>((x, y) => x.ModuleCode == y.Code)
                 .LeftJoin<AccountEntity>((x, y, z) => x.CreatedBy.Equals(z.Id))
                 .Select((x, y, z) => new { x, ModuleName = y.Name, Creator = z.Name }).PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<IList<PermissionEntity>> QueryByMenu(string menuCode)
        {
            return Db.Find().InnerJoin<MenuPermissionEntity>((p, m) => p.Code == m.PermissionCode).Where((p, m) => m.MenuCode == menuCode)
                .ToListAsync();
        }

        public Task<IList<PermissionEntity>> QueryByButton(string buttonCode)
        {
            return Db.Find().InnerJoin<ButtonPermissionEntity>((p, m) => p.Code == m.PermissionCode).Where((p, m) => m.ButtonCode == buttonCode)
                .ToListAsync();
        }

        public async Task<IList<PermissionEntity>> QueryByAccount(Guid accountId)
        {
            var list = new List<PermissionEntity>();
            var menuPermissionListTask = Db.Find().InnerJoin<MenuPermissionEntity>((x, y) => x.Code == y.PermissionCode)
                .InnerJoin<MenuEntity>((x, y, z) => y.MenuCode == z.RouteName)
                .InnerJoin<RoleMenuEntity>((x, y, z, m) => z.Id == m.MenuId)
                .InnerJoin<AccountRoleEntity>((x, y, z, m, n) => m.RoleId == n.RoleId && n.AccountId == accountId)
                .ToListAsync();

            var btnPermissionListTask = Db.Find().InnerJoin<ButtonPermissionEntity>((x, y) => x.Code == y.PermissionCode)
                .InnerJoin<ButtonEntity>((x, y, z) => y.ButtonCode == z.Code)
                .InnerJoin<RoleMenuButtonEntity>((x, y, z, m) => z.Id == m.ButtonId)
                .InnerJoin<AccountRoleEntity>((x, y, z, m, n) => m.RoleId == n.RoleId && n.AccountId == accountId)
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

        public Task<bool> UpdateForSync(PermissionEntity entity, IDbTransaction transaction)
        {
            return Db.Find(m => m.ModuleCode == entity.ModuleCode && m.Controller == entity.Controller && m.Action == entity.Action)
                .UseTran(transaction)
                .UpdateAsync(m => new PermissionEntity { Name = entity.Name });
        }
    }
}
