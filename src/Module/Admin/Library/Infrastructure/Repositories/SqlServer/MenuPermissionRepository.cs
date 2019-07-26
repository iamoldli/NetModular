using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Module.Admin.Domain.MenuPermission;

namespace Nm.Module.Admin.Infrastructure.Repositories.SqlServer
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

        public Task<bool> DeleteByPermissionId(Guid permissionId, IDbTransaction transaction)
        {
            return Db.Find(e => e.PermissionId == permissionId).UseTran(transaction).DeleteAsync();
        }

        public Task<bool> DeleteByMenuId(Guid menuId, IDbTransaction transaction)
        {
            return Db.Find(e => e.MenuId == menuId).UseTran(transaction).DeleteAsync();
        }

        public Task<IList<MenuPermissionEntity>> GetListByMenuId(Guid menuId)
        {
            return Db.Find(e => e.MenuId == menuId).ToListAsync();
        }
    }
}
