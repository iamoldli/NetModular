using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Admin.Domain.MenuPermission;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class MenuPermissionRepository : RepositoryAbstract<MenuPermissionEntity>, IMenuPermissionRepository
    {
        public MenuPermissionRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> ExistsBindPermission(Guid permissionId)
        {
            return Db.Find(m => m.PermissionId.Equals(permissionId)).ExistsAsync();
        }

        public Task<bool> DeleteByPermissionId(Guid permissionId)
        {
            return Db.Find(e => e.PermissionId == permissionId).DeleteAsync();
        }

        public Task<bool> DeleteByMenuId(Guid menuId)
        {
            return Db.Find(e => e.MenuId == menuId).DeleteAsync();
        }

        public Task<IList<MenuPermissionEntity>> GetListByMenuId(Guid menuId)
        {
            return Db.Find(e => e.MenuId == menuId).ToListAsync();
        }
    }
}
