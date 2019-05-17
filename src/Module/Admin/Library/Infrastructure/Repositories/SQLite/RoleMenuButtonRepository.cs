using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Module.Admin.Domain.Button;
using Nm.Module.Admin.Domain.RoleMenuButton;

namespace Nm.Module.Admin.Infrastructure.Repositories.SQLite
{
    public class RoleMenuButtonRepository : SqlServer.RoleMenuButtonRepository
    {
        public RoleMenuButtonRepository(IDbContext context) : base(context)
        {
        }

        public override Task<IList<ButtonEntity>> Query(Guid roleId, Guid menuId)
        {
            return Db.DbContext.Set<ButtonEntity>().Find()
                .LeftJoin<RoleMenuButtonEntity>((x, y) => x.Id == y.ButtonId && y.RoleId == roleId)
                .Where((x, y) => x.MenuId == menuId)
                .Select((x, y) => new { x, y.RoleId })
                .ToListAsync<ButtonEntity>();
        }
    }
}
