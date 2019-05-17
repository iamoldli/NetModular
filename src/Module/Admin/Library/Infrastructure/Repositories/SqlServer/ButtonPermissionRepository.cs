using System;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Module.Admin.Domain.ButtonPermission;

namespace Nm.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class ButtonPermissionRepository : RepositoryAbstract<ButtonPermissionEntity>, IButtonPermissionRepository
    {
        public ButtonPermissionRepository(IDbContext context) : base(context)
        {
        }

        public Task<bool> ExistsBindPermission(Guid permissionId)
        {
            return Db.Find(m => m.PermissionId.Equals(permissionId)).ExistsAsync();
        }

        public Task<bool> RemoveByButtonId(Guid buttonId)
        {
            return Db.Find(e => e.ButtonId == buttonId).DeleteAsync();

        }
    }
}
