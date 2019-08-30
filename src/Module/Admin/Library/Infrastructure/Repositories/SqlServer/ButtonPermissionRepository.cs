using System.Data;
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

        public Task<bool> ExistsBindPermission(string permissionCode)
        {
            return Db.Find(m => m.PermissionCode.Equals(permissionCode)).ExistsAsync();
        }

        public Task<bool> DeleteByButton(string buttonCode, IDbTransaction transaction)
        {
            return Db.Find(e => e.ButtonCode == buttonCode).UseTran(transaction).DeleteAsync();
        }
    }
}
