using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Web.Attributes;
using NetModular.Module.Admin.Application.RoleService;
using NetModular.Module.Admin.Application.RoleService.ViewModels;
using NetModular.Module.Admin.Domain.Role.Models;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("角色管理")]
    public class RoleController : Web.ModuleController
    {
        private readonly IRoleService _service;
        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]RoleQueryModel model)
        {
            return _service.Query(model);
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
        [Description("获取绑定页面列表")]
        public Task<IResultModel> BindPages([BindRequired] Guid id)
        {
            return _service.QueryBindPages(id);
        }

        [HttpPost]
        [Description("绑定页面列表")]
        public Task<IResultModel> BindPages(RolePageBindModel model)
        {
            return _service.BindPages(model);
        }

        [HttpGet]
        [Description("获取绑定菜单列表")]
        public Task<IResultModel> BindMenus([BindRequired]Guid id)
        {
            return _service.QueryBindMenus(id);
        }

        [HttpPost]
        [Description("绑定菜单")]
        public Task<IResultModel> BindMenus(RoleMenuBindModel model)
        {
            return _service.BindMenus(model);
        }

        [HttpGet]
        [Common]
        public Task<IResultModel> Select()
        {
            return _service.Select();
        }

        [HttpGet]
        [Description("查询平台权限列表")]
        public Task<IResultModel> BindPlatformPermissions([BindRequired]Guid roleId, Platform platform)
        {
            return _service.QueryPlatformPermissions(roleId, platform);
        }

        [HttpPost]
        [Description("绑定平台权限列表")]
        public Task<IResultModel> BindPlatformPermissions(RolePermissionBindModel model)
        {
            return _service.BindPlatformPermissions(model);
        }
    }
}
