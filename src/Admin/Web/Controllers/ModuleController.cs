using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetModular.Lib.Auth.Web.Attributes;
using NetModular.Module.Admin.Application.ModuleService;
using NetModular.Module.Admin.Web.Core;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("模块信息")]
    public class ModuleController : Web.ModuleController
    {
        private readonly IModuleService _service;
        private readonly PermissionHelper _permissionHelper;
        public ModuleController(IModuleService service, PermissionHelper permissionHelper)
        {
            _service = service;
            _permissionHelper = permissionHelper;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query()
        {
            return _service.Query();
        }

        [HttpGet]
        [Common]
        [Description("下拉列表")]
        public IResultModel Select()
        {
            return _service.Select();
        }

        [HttpPost]
        [Description("同步")]
        public Task<IResultModel> Sync()
        {
            return _service.Sync(_permissionHelper.GetAllPermission());
        }
    }
}
