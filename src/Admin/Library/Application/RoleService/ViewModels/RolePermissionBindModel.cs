using System;
using System.Collections.Generic;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Utils.Core.Validations;

namespace NetModular.Module.Admin.Application.RoleService.ViewModels
{
    public class RolePermissionBindModel
    {
        [NotEmpty(ErrorMessage = "请选择角色")]
        public Guid RoleId { get; set; }

        public Platform Platform { get; set; }

        public List<string> Permissions { get; set; }
    }
}
