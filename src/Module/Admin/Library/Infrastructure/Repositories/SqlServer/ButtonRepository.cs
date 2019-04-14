using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Pagination;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.AccountRole;
using NetModular.Module.Admin.Domain.Button;
using NetModular.Module.Admin.Domain.RoleMenuButton;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class ButtonRepository : RepositoryAbstract<Button>, IButtonRepository
    {
        public ButtonRepository(IDbContext context) : base(context)
        {
        }

        public Task<IList<Button>> Query(Paging paging, Guid menuId, string name = null)
        {
            var query = Db.Find(m => m.MenuId == menuId).WhereIf(name.NotNull(), m => m.Name.Contains(name));
            return query.LeftJoin<Account>((x, y) => x.CreatedBy == y.Id).Select((x, y) => new { x, Creator = y.Name })
                .PaginationAsync(paging);
        }

        public Task<bool> Exists(string code, Guid? id = null)
        {
            var query = Db.Find(m => m.Code == code);
            query.WhereIf(id != null, m => m.Id != id);
            return query.ExistsAsync();
        }

        public Task<IList<Button>> QueryByMenu(Guid menuId)
        {
            return Db.Find(m => m.MenuId == menuId).ToListAsync();
        }

        public Task<IList<string>> QueryCodeByAccount(Guid accountId)
        {
            return Db.Find()
                .InnerJoin<RoleMenuButton>((x, y) => x.Id == y.ButtonId)
                .InnerJoin<AccountRole>((x, y, z) => y.RoleId == z.RoleId && z.AccountId == accountId)
                .Select((x, y, z) => x.Code)
                .ToListAsync<string>();
        }

        public Task<bool> DeleteByMenu(Guid menuId)
        {
            return Db.Find(m => m.MenuId == menuId).DeleteAsync();
        }

        public Task<bool> UpdateForSync(Button button)
        {
            return Db.Find(m => m.MenuId == button.MenuId && m.Code == button.Code)
                .UpdateAsync(m => new Button { Icon = button.Icon, Name = button.Name });
        }
    }
}
