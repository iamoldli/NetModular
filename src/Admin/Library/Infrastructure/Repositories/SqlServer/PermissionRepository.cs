using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Data.Query;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.AccountRole;
using NetModular.Module.Admin.Domain.Button;
using NetModular.Module.Admin.Domain.ButtonPermission;
using NetModular.Module.Admin.Domain.Menu;
using NetModular.Module.Admin.Domain.MenuPermission;
using NetModular.Module.Admin.Domain.ModuleInfo;
using NetModular.Module.Admin.Domain.Permission;
using NetModular.Module.Admin.Domain.Permission.Models;
using NetModular.Module.Admin.Domain.RoleMenu;
using NetModular.Module.Admin.Domain.RoleMenuButton;
using NetModular.Module.Admin.Domain.RolePlatformPermission;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class PermissionRepository : RepositoryAbstract<PermissionEntity>, IPermissionRepository
    {
        public PermissionRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> ExistsWidthModule(string moduleCode)
        {
            return ExistsAsync(m => m.ModuleCode.Equals(moduleCode));
        }

        public async Task<IList<PermissionEntity>> Query(PermissionQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find();

            query.WhereNotNull(model.ModuleCode, m => m.ModuleCode.Equals(model.ModuleCode));
            query.WhereNotNull(model.Name, m => m.Name.Contains(model.Name));
            query.WhereNotNull(model.Controller, m => m.Controller.Equals(model.Controller));
            query.WhereNotNull(model.Action, m => m.Action.Equals(model.Action));

            var joinQuery = query.LeftJoin<ModuleInfoEntity>((x, y) => x.ModuleCode == y.Code)
                .LeftJoin<AccountEntity>((x, y, z) => x.CreatedBy.Equals(z.Id));

            if (!paging.OrderBy.Any())
            {
                joinQuery.OrderByDescending((x, y, z) => x.Id);
            }

            joinQuery.Select((x, y, z) => new { x, ModuleName = y.Name, Creator = z.Name });

            var list = await joinQuery.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public async Task<IList<string>> QueryCodeByAccount(Guid accountId, Platform platform)
        {
            var list = new List<string>();
            if (platform == Platform.Web)
            {
                var menuPermissionListTask = Db.Find()
                    .InnerJoin<MenuPermissionEntity>((x, y) => x.Code == y.PermissionCode)
                    .InnerJoin<MenuEntity>((x, y, z) => y.MenuCode == z.RouteName)
                    .InnerJoin<RoleMenuEntity>((x, y, z, m) => z.Id == m.MenuId)
                    .InnerJoin<AccountRoleEntity>((x, y, z, m, n) => m.RoleId == n.RoleId && n.AccountId == accountId)
                    .Select((x, y, z, m, n) => x.Code)
                    .ToListAsync<string>();

                var btnPermissionListTask = Db.Find()
                    .InnerJoin<ButtonPermissionEntity>((x, y) => x.Code == y.PermissionCode)
                    .InnerJoin<ButtonEntity>((x, y, z) => y.ButtonCode == z.Code)
                    .InnerJoin<RoleMenuButtonEntity>((x, y, z, m) => z.Id == m.ButtonId)
                    .InnerJoin<AccountRoleEntity>((x, y, z, m, n) => m.RoleId == n.RoleId && n.AccountId == accountId)
                    .Select((x, y, z, m, n) => x.Code)
                    .ToListAsync<string>();

                var menuPermissionList = await menuPermissionListTask;
                var btnPermissionList = await btnPermissionListTask;

                foreach (var p in menuPermissionList)
                {
                    if (!list.Contains(p))
                    {
                        list.Add(p);
                    }
                }

                foreach (var p in btnPermissionList)
                {
                    if (!list.Contains(p))
                    {
                        list.Add(p);
                    }
                }
                return list;
            }

            //其他平台查询方式
            return await Db.Find().InnerJoin<RolePlatformPermissionEntity>((x, y) => x.Code == y.PermissionCode)
                .InnerJoin<AccountRoleEntity>((x, y, z) => y.RoleId == z.RoleId && z.AccountId == accountId)
                .Select((x, y, z) => x.Code)
                .ToListAsync<string>();
        }
    }
}
