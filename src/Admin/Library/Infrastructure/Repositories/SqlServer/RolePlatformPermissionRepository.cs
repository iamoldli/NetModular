using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Admin.Domain.RolePlatformPermission;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class RolePlatformPermissionRepository : RepositoryAbstract<RolePlatformPermissionEntity>, IRolePlatformPermissionRepository
    {
        public RolePlatformPermissionRepository(IDbContext context) : base(context)
        {
        }

        public Task<IList<RolePlatformPermissionEntity>> Query(Guid roleId, Platform platform)
        {
            return Db.Find(m => m.RoleId == roleId && m.Platform == platform).ToListAsync();
        }

        public Task<bool> Clear(Guid roleId, Platform platform)
        {
            return Db.Find(m => m.RoleId == roleId && m.Platform == platform).DeleteAsync();
        }
    }
}
