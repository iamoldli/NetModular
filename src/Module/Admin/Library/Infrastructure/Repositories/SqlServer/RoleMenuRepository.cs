using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Module.Admin.Domain.Menu;
using Nm.Module.Admin.Domain.RoleMenu;

namespace Nm.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class RoleMenuRepository : RepositoryAbstract<RoleMenuEntity>, IRoleMenuRepository
    {
        public RoleMenuRepository(IDbContext context) : base(context)
        {
        }
        public Task<IList<RoleMenuEntity>> GetByRoleId(Guid roleId)
        {
            return Db.Find(e => e.RoleId == roleId)
                .LeftJoin<MenuEntity>((x, y) => x.MenuId == y.Id)
                .Select((x, y) => new { x, MenuType = y.Type })
                .ToListAsync();
        }

        public Task<bool> DeleteByMenuId(Guid menuId)
        {
            return Db.Find(e => e.MenuId == menuId).DeleteAsync();
        }

        public Task<bool> ExistsWidthMenuId(Guid menuId)
        {
            return Db.Find(e => e.MenuId == menuId).ExistsAsync();
        }

        public Task<bool> DeleteByRoleId(Guid roleId)
        {
            return Db.Find(e => e.RoleId == roleId).DeleteAsync();
        }
    }
}
