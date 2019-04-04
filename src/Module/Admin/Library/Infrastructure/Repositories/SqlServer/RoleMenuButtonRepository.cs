using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Admin.Domain.Button;
using NetModular.Module.Admin.Domain.RoleMenuButton;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class RoleMenuButtonRepository : RepositoryAbstract<RoleMenuButton>, IRoleMenuButtonRepository
    {
        public RoleMenuButtonRepository(IDbContext context) : base(context)
        {
        }

        public Task<bool> DeleteByRole(Guid roleId)
        {
            return Db.Find(m => m.RoleId == roleId).DeleteAsync();
        }

        public Task<bool> DeleteByMenu(Guid menuId)
        {
            return Db.Find(m => m.MenuId == menuId).DeleteAsync();
        }

        public Task<bool> DeleteByButton(Guid buttonId)
        {
            return Db.Find(m => m.ButtonId == buttonId).DeleteAsync();
        }

        public Task<IList<Button>> Query(Guid roleId, Guid menuId)
        {
            return Db.Find()
                .RightJoin<Button>((x, y) => x.ButtonId == y.Id && x.RoleId == roleId)
                .Where((x, y) => y.MenuId == menuId)
                .Select((x, y) => new { x.RoleId, y })
                .ToListAsync<Button>();
        }

        public Task<bool> Exists(RoleMenuButton entity)
        {
            return Db.Find(m => m.RoleId == entity.RoleId && m.MenuId == entity.MenuId && m.ButtonId == entity.ButtonId)
                .ExistsAsync();
        }

        public Task<bool> Delete(RoleMenuButton entity)
        {
            return Db.Find(m => m.RoleId == entity.RoleId && m.MenuId == entity.MenuId && m.ButtonId == entity.ButtonId)
                .DeleteAsync();
        }

        public Task<bool> Delete(Guid roleId, Guid menuId)
        {
            return Db.Find(m => m.RoleId == roleId && m.MenuId == menuId)
                .DeleteAsync();
        }

        public Task<bool> ExistsWidthButton(Guid buttonId)
        {
            return Db.Find(m => m.ButtonId == buttonId).ExistsAsync();
        }
    }
}
