using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Module.Admin.Domain.Button;
using Nm.Module.Admin.Domain.Menu;
using Nm.Module.Admin.Domain.RoleMenuButton;

namespace Nm.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class RoleMenuButtonRepository : RepositoryAbstract<RoleMenuButtonEntity>, IRoleMenuButtonRepository
    {
        public RoleMenuButtonRepository(IDbContext context) : base(context)
        {
        }

        public Task<bool> DeleteByRole(Guid roleId, IDbTransaction transaction)
        {
            return Db.Find(m => m.RoleId == roleId).UseTran(transaction).DeleteAsync();
        }

        public Task<bool> DeleteByMenu(Guid menuId, IDbTransaction transaction)
        {
            return Db.Find(m => m.MenuId == menuId).UseTran(transaction).DeleteAsync();
        }

        public Task<bool> DeleteByButton(Guid buttonId, IDbTransaction transaction)
        {
            return Db.Find(m => m.ButtonId == buttonId).UseTran(transaction).DeleteAsync();
        }

        public virtual Task<IList<ButtonEntity>> Query(Guid roleId, Guid menuId)
        {
            return DbContext.Set<ButtonEntity>().Find()
                 .InnerJoin<MenuEntity>((x, y) => x.MenuCode == y.RouteName && y.Id == menuId)
                 .LeftJoin<RoleMenuButtonEntity>((x, y, z) => x.Id == z.ButtonId && z.RoleId == roleId)
                 .Select((x, y, z) => new { x, z.RoleId })
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

        public Task<bool> Delete(Guid roleId, Guid menuId, IDbTransaction transaction)
        {
            return Db.Find(m => m.RoleId == roleId && m.MenuId == menuId).UseTran(transaction).DeleteAsync();
        }

        public Task<bool> ExistsWidthButton(Guid buttonId)
        {
            return Db.Find(m => m.ButtonId == buttonId).ExistsAsync();
        }
    }
}
