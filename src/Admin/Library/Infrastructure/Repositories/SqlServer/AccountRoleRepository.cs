using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.AccountRole;
using NetModular.Module.Admin.Domain.Role;
using NetModular.Module.Admin.Domain.RoleMenu;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
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

        public Task<bool> DeleteByAccount(Guid accountId, IUnitOfWork uow)
        {
            return Db.Find(m => m.AccountId == accountId).UseUow(uow).DeleteAsync();
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

        public Task<bool> ExistsByRole(Guid roleId)
        {
            return Db.Find(m => m.RoleId == roleId).InnerJoin<AccountEntity>((x, y) => x.AccountId == y.Id && y.Deleted == false).ExistsAsync();
        }
    }
}
