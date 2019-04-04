using System;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Admin.Domain.ButtonPermission;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class ButtonPermissionRepository : RepositoryAbstract<ButtonPermission>, IButtonPermissionRepository
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
