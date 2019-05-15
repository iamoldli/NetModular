using System.Collections.Generic;
using NetModular.Module.Admin.Domain.Permission;

namespace NetModular.Module.Admin.Application.SystemService.ViewModels
{
    public class SystemInstallModel
    {
        public List<PermissionEntity> Permissions { get; set; }
    }
}
