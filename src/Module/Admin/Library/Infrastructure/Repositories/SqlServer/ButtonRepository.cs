using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Admin.Domain.AccountRole;
using Nm.Module.Admin.Domain.Button;
using Nm.Module.Admin.Domain.Button.Models;
using Nm.Module.Admin.Domain.RoleMenuButton;

namespace Nm.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class ButtonRepository : RepositoryAbstract<ButtonEntity>, IButtonRepository
    {
        public ButtonRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<ButtonEntity>> Query(ButtonQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find(m => m.MenuId == model.MenuId)
                .WhereIf(model.Name.NotNull(), m => m.Name.Contains(model.Name));

            var list = await query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .Select((x, y) => new { x, Creator = y.Name })
                .PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<bool> Exists(string code, Guid? id = null)
        {
            var query = Db.Find(m => m.Code == code);
            query.WhereIf(id != null, m => m.Id != id);
            return query.ExistsAsync();
        }

        public Task<IList<ButtonEntity>> QueryByMenu(Guid menuId)
        {
            return Db.Find(m => m.MenuId == menuId).ToListAsync();
        }

        public Task<IList<string>> QueryCodeByAccount(Guid accountId)
        {
            return Db.Find()
                .InnerJoin<RoleMenuButtonEntity>((x, y) => x.Id == y.ButtonId)
                .InnerJoin<AccountRoleEntity>((x, y, z) => y.RoleId == z.RoleId && z.AccountId == accountId)
                .Select((x, y, z) => x.Code)
                .ToListAsync<string>();
        }

        public Task<bool> DeleteByMenu(Guid menuId)
        {
            return Db.Find(m => m.MenuId == menuId).DeleteAsync();
        }

        public Task<bool> UpdateForSync(ButtonEntity button)
        {
            return Db.Find(m => m.MenuId == button.MenuId && m.Code == button.Code)
                .UpdateAsync(m => new ButtonEntity { Icon = button.Icon, Name = button.Name });
        }
    }
}
