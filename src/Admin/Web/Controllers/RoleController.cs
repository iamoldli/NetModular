using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nm.Lib.Auth.Web.Attributes;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Application.RoleService;
using Nm.Module.Admin.Application.RoleService.ViewModels;
using Nm.Module.Admin.Domain.Role.Models;

namespace Nm.Module.Admin.Web.Controllers
{
    [Description("角色管理")]
    public class RoleController : ModuleController
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]RoleQueryModel model)
        {
            return await _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(RoleAddModel model)
        {
            return _service.Add(model);
        }

        [HttpDelete]
        [Description("删除")]
        public Task<IResultModel> Delete([BindRequired]Guid id)
        {
            return _service.Delete(id);
        }

        [HttpGet]
        [Description("编辑")]
        public Task<IResultModel> Edit([BindRequired]Guid id)
        {
            return _service.Edit(id);
        }

        [HttpPost]
        [Description("修改")]
        public Task<IResultModel> Update(RoleUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpGet]
        [Description("获取角色的关联菜单列表")]
        public Task<IResultModel> MenuList([BindRequired] Guid id)
        {
            return _service.MenuList(id);
        }

        [HttpPost]
        [Description("绑定菜单")]
        public Task<IResultModel> BindMenu(RoleMenuBindModel model)
        {
            return _service.BindMenu(model);
        }

        [HttpGet]
        [Description("获取角色关联的菜单按钮列表")]
        public Task<IResultModel> MenuButtonList([BindRequired]Guid id, [BindRequired]Guid menuId)
        {
            return _service.MenuButtonList(id, menuId);
        }

        [HttpPost]
        [Description("绑定菜单按钮")]
        public Task<IResultModel> BindMenuButton(RoleMenuButtonBindModel model)
        {
            return _service.BindMenuButton(model);
        }

        [HttpGet]
        [Description("下拉列表数据")]
        [Common]
        public Task<IResultModel> Select()
        {
            return _service.Select();
        }
    }
}
