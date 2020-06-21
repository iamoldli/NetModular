using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Admin.Domain.AccountRole;
using NetModular.Module.Admin.Domain.RoleButton;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class RoleButtonRepository : RepositoryAbstract<RoleButtonEntity>, IRoleButtonRepository
    {
        public RoleButtonRepository(IDbContext context) : base(context)
        {
        }

        public Task<IList<RoleButtonEntity>> QueryButtonCodes(Guid roleId)
        {
            return Db.Find(m => m.RoleId == roleId).ToListAsync();
        }

        public Task<IList<string>> QueryButtonCodes(Guid roleId, string pageCode)
        {
            return Db.Find(m => m.RoleId == roleId && m.PageCode == pageCode).Select(m => m.ButtonCode).ToListAsync<string>();
        }

        public Task<IList<string>> QueryButtonCodesByAccount(Guid accountId)
        {
            return Db.Find()
                .InnerJoin<AccountRoleEntity>((x, y) => x.RoleId == y.RoleId && y.AccountId == accountId)
                .Select((x, y) => x.ButtonCode)
                .ToListAsync<string>();
        }

        public Task<bool> DeleteByRole(Guid roleId, IUnitOfWork uow = null)
        {
            return Db.Find(m => m.RoleId == roleId).UseUow(uow).DeleteAsync();
        }
    }
}
