using System.Collections.Generic;
using Tm.Module.Admin.Domain.Permission;

namespace Tm.Module.Admin.Application.SystemService.ViewModels
{
    public class SystemInstallModel
    {
        public List<PermissionEntity> Permissions { get; set; }
    }
}
