using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Module.Admin.Domain.Button;
using Nm.Module.Admin.Domain.RoleMenuButton;

namespace Nm.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class RoleMenuButtonRepository : RepositoryAbstract<RoleMenuButtonEntity>, IRoleMenuButtonRepository
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

        public virtual Task<IList<ButtonEntity>> Query(Guid roleId, Guid menuId)
        {
            return Db.Find()
                .RightJoin<ButtonEntity>((x, y) => x.ButtonId == y.Id && x.RoleId == roleId)
                .Where((x, y) => y.MenuId == menuId)
                .Select((x, y) => new { x.RoleId, y })
                .ToListAsync<ButtonEntity>();
        }

        public Task<bool> Exists(RoleMenuButtonEntity entity)
        {
            return Db.Find(m => m.RoleId == entity.RoleId && m.MenuId == entity.MenuId && m.ButtonId == entity.ButtonId)
                .ExistsAsync();
        }

        public Task<bool> Delete(RoleMenuButtonEntity entity)
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
