using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.ButtonService;
using NetModular.Module.Admin.Application.ButtonService.ViewModels;

namespace NetModular.Module.Admin.Web.Controllers
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
        [Description("添加")]
        public Task<IResultModel> Add(ButtonAddModel model)
        {
            return _service.Add(model);
        }

        [HttpGet]
        [Description("编辑")]
        public Task<IResultModel> Edit([BindRequired]Guid id)
        {
            return _service.Edit(id);
        }

        [HttpPost]
        [Description("更新")]
        public Task<IResultModel> Update(ButtonUpdateModel model)
        {
            return _service.Update(model);
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
