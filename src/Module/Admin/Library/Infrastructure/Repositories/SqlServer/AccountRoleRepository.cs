using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Module.Admin.Domain.Account;
using Tm.Module.Admin.Domain.AccountRole;
using Tm.Module.Admin.Domain.Role;
using Tm.Module.Admin.Domain.RoleMenu;
using Tm.Module.Admin.Domain.RoleMenuButton;

namespace Tm.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class AccountRoleRepository : RepositoryAbstract<AccountRoleEntity>, IAccountRoleRepository
    {
        public AccountRoleRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> Delete(Guid accountId, Guid roleId)
        {
            return Db.Find(m => m.AccountId == accountId && m.RoleId == roleId).DeleteAsync();
        }

        public Task<bool> DeleteByAccount(Guid accountId, IDbTransaction transaction)
        {
            return Db.Find(m => m.AccountId == accountId).UseTran(transaction).DeleteAsync();
        }

        public Task<bool> Exists(Guid accountId, Guid roleId)
        {
            return Db.Find(m => m.AccountId == accountId && m.RoleId == roleId).ExistsAsync();
        }

        public Task<IList<RoleEntity>> QueryRole(Guid accountId)
        {
            return Db.Find(m => m.AccountId == accountId)
                .InnerJoin<RoleEntity>((x, y) => x.RoleId == y.Id)
                .Select((x, y) => new { y })
                .ToListAsync<RoleEntity>();
        }

        public Task<IList<AccountRoleEntity>> QueryByRole(Guid roleId)
        {
            return Db.Find(m => m.RoleId == roleId).ToListAsync();
        }

        public Task<IList<AccountRoleEntity>> QueryByMenu(Guid menuId)
        {
            return Db.Find()
                .InnerJoin<RoleMenuEntity>((x, y) => x.RoleId == y.RoleId)
                .Where((x, y) => y.MenuId == menuId)
                .ToListAsync();
        }

        public Task<IList<AccountRoleEntity>> QueryByButton(Guid buttonId)
        {
            return Db.Find()
                .InnerJoin<RoleMenuButtonEntity>((x, y) => x.RoleId == y.RoleId && y.ButtonId == buttonId)
                .ToListAsync();
        }

        public Task<bool> ExistsByRole(Guid roleId)
        {
            return Db.Find(m => m.RoleId == roleId).InnerJoin<AccountEntity>((x, y) => x.AccountId == y.Id && y.Deleted == false).ExistsAsync();
        }
    }
}
