using System;
using System.Collections.Generic;
using System.Data;
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

            var query = Db.Find(m => m.MenuCode == model.MenuCode)
                .WhereIf(model.Name.NotNull(), m => m.Name.Contains(model.Name));

            var list = await query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .Select((x, y) => new { x, Creator = y.Name })
                .PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<IList<ButtonEntity>> QueryByMenu(string menuCode, IDbTransaction transaction)
        {
            return Db.Find(m => m.MenuCode == menuCode).UseTran(transaction).ToListAsync();
        }

        public Task<IList<string>> QueryCodeByAccount(Guid accountId)
        {
            return Db.Find()
                .InnerJoin<RoleMenuButtonEntity>((x, y) => x.Id == y.ButtonId)
                .InnerJoin<AccountRoleEntity>((x, y, z) => y.RoleId == z.RoleId && z.AccountId == accountId)
                .Select((x, y, z) => x.Code)
                .ToListAsync<string>();
        }

        public Task<bool> ExistsByCode(string code, IDbTransaction transaction)
        {
            return Db.Find(m => m.Code == code).ExistsAsync();
        }
    }
}
