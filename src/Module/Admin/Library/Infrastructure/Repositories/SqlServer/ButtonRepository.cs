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

        public Task<bool> Exists(ButtonEntity entity, IDbTransaction transaction = null)
        {
            var query = Db.Find(m => m.Code == entity.Code);
            query.WhereIf(entity.Id.NotEmpty(), m => m.Id != entity.Id);
            query.UseTran(transaction);
            return query.ExistsAsync();
        }

        public Task<IList<ButtonEntity>> QueryByMenu(string menuCode)
        {
            return Db.Find(m => m.MenuCode == menuCode).ToListAsync();
        }

        public Task<IList<string>> QueryCodeByAccount(Guid accountId)
        {
            return Db.Find()
                .InnerJoin<RoleMenuButtonEntity>((x, y) => x.Id == y.ButtonId)
                .InnerJoin<AccountRoleEntity>((x, y, z) => y.RoleId == z.RoleId && z.AccountId == accountId)
                .Select((x, y, z) => x.Code)
                .ToListAsync<string>();
        }

        public Task<bool> DeleteByMenu(string menuCode, IDbTransaction transaction)
        {
            return Db.Find(m => m.MenuCode == menuCode).UseTran(transaction).DeleteAsync();
        }

        public Task<bool> UpdateForSync(ButtonEntity button, IDbTransaction transaction)
        {
            return Db.Find(m => m.MenuCode == button.MenuCode && m.Code == button.Code)
                .UseTran(transaction)
                .UpdateAsync(m => new ButtonEntity { Icon = button.Icon, Name = button.Name });
        }
    }
}
