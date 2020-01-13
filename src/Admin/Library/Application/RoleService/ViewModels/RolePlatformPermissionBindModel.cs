using System;
using System.Collections.Generic;
using NetModular.Lib.Auth.Abstractions;

namespace NetModular.Module.Admin.Application.RoleService.ViewModels
{
    public class RolePlatformPermissionBindModel
    {
        public Guid RoleId { get; set; }

        public Platform Platform { get; set; }

        public List<string> Permissions { get; set; }
    }
}
