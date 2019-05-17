using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Application.ButtonService;
using Nm.Module.Admin.Application.ButtonService.ViewModels;
using Nm.Module.Admin.Domain.Button.Models;

namespace Nm.Module.Admin.Web.Controllers
{
    [Description("按钮管理")]
    public class ButtonController : ModuleController
    {
        private readonly IButtonService _service;

        public ButtonController(IButtonService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]ButtonQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("同步")]
        public Task<IResultModel> Sync(ButtonSyncModel model)
        {
            return _service.Sync(model);
        }

        [HttpDelete]
        [Description("删除")]
        public Task<IResultModel> Delete([BindRequired]Guid id)
        {
            return _service.Delete(id);
        }

        [HttpGet]
        [Description("获取按钮绑定的权限列表")]
        public Task<IResultModel> PermissionList([BindRequired]Guid id)
        {
            return _service.QueryPermissionList(id);
        }

        [HttpPost]
        [Description("绑定权限")]
        public async Task<IResultModel> BindPermission(ButtonBindPermissionModel model)
        {
            return await _service.BindPermission(model);
        }
    }
}
