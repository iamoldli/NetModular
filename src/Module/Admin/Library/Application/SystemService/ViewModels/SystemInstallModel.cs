using System.Collections.Generic;
using Nm.Module.Admin.Domain.Permission;

namespace Nm.Module.Admin.Application.SystemService.ViewModels
{
    public class SystemInstallModel
    {
        public List<PermissionEntity> Permissions { get; set; }
    }
}
