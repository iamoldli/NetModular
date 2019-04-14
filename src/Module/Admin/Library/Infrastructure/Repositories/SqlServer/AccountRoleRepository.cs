using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.AccountRole;
using NetModular.Module.Admin.Domain.Role;
using NetModular.Module.Admin.Domain.RoleMenu;
using NetModular.Module.Admin.Domain.RoleMenuButton;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class AccountRoleRepository : RepositoryAbstract<AccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> Delete(Guid accountId, Guid roleId)
        {
            return Db.Find(m => m.AccountId == accountId && m.RoleId == roleId).DeleteAsync();
        }

        public Task<bool> DeleteByAccount(Guid accountId)
        {
            return Db.Find(m => m.AccountId == accountId).DeleteAsync();
        }

        public Task<bool> Exists(Guid accountId, Guid roleId)
        {
            return Db.Find(m => m.AccountId == accountId && m.RoleId == roleId).ExistsAsync();
        }

        public Task<IList<Role>> QueryRole(Guid accountId)
        {
            return Db.Find(m => m.AccountId == accountId)
                .InnerJoin<Role>((x, y) => x.RoleId == y.Id)
                .Select((x, y) => new { y })
                .ToListAsync<Role>();
        }

        public Task<IList<AccountRole>> QueryByRole(Guid roleId)
        {
            return Db.Find(m => m.RoleId == roleId).ToListAsync();
        }

        public Task<IList<AccountRole>> QueryByMenu(Guid menuId)
        {
            return Db.Find()
                .InnerJoin<RoleMenu>((x, y) => x.RoleId == y.RoleId)
                .Where((x, y) => y.MenuId == menuId)
                .ToListAsync();
        }

        public Task<IList<AccountRole>> QueryByButton(Guid buttonId)
        {
            return Db.Find()
                .InnerJoin<RoleMenuButton>((x, y) => x.RoleId == y.RoleId && y.ButtonId == buttonId)
                .ToListAsync();
        }

        public Task<bool> ExistsByRole(Guid roleId)
        {
            return Db.Find(m => m.RoleId == roleId).InnerJoin<Account>((x, y) => x.AccountId == y.Id && y.Deleted == false).ExistsAsync();
        }
    }
}
