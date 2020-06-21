using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Admin.Domain.AccountRole;
using NetModular.Module.Admin.Domain.RolePermission;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class RolePermissionRepository : RepositoryAbstract<RolePermissionEntity>, IRolePermissionRepository
    {
        public RolePermissionRepository(IDbContext context) : base(context)
        {
        }

        public Task<IList<string>> QueryByRole(Guid roleId, Platform platform)
        {
            return Db.Find(m => m.RoleId == roleId && m.Platform == platform).Select(m => m.PermissionCode).ToListAsync<string>();
        }

        public Task<IList<string>> QueryByAccount(Guid accountId, Platform platform)
        {
            return Db.Find(m => m.Platform == platform)
                .InnerJoin<AccountRoleEntity>((x, y) => x.RoleId == y.RoleId && y.AccountId == accountId)
                .Select((x, y) => x.PermissionCode)
                .ToListAsync<string>();
        }

        public Task<bool> DeleteByRole(Guid roleId, Platform platform, IUnitOfWork uow = null)
        {
            return Db.Find(m => m.RoleId == roleId && m.Platform == platform).UseUow(uow).DeleteAsync();
        }
    }
}
