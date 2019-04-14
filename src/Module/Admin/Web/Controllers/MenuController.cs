using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Auth.Abstractions.Attributes;
using NetModular.Lib.Module.Abstractions.Attributes;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.MenuService;
using NetModular.Module.Admin.Application.MenuService.ViewModels;
using NetModular.Module.Admin.Domain.Menu;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("菜单管理")]
    public class MenuController : ModuleController
    {
        private readonly IMenuService _service;

        public MenuController(IMenuService menuService)
        {
            _service = menuService;
        }

        [HttpGet]
        [Description("菜单树")]
        public Task<IResultModel> Tree()
        {
            return _service.GetTree();
        }

        [HttpGet]
        [Description("查询菜单列表")]
        public Task<IResultModel> Query([FromQuery]MenuQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpGet]
        [Common]
        [DisableAuditing]
        [Description("链接菜单打开方式下拉列表")]
        public IResultModel TargetSelect()
        {
            return ResultModel.Success(EnumExtensions.ToResult<MenuTarget>(true));
        }

        [HttpPost]
        [Description("添加")]
        public async Task<IResultModel> Add(MenuAddModel model)
        {
            return await _service.Add(model);
        }

        [HttpDelete]
        [Description("删除")]
        public async Task<IResultModel> Delete([BindRequired]Guid id)
        {
            return await _service.Delete(id);
        }

        [HttpGet]
        [Description("编辑")]
        public Task<IResultModel> Edit([BindRequired]Guid id)
        {
            return _service.Edit(id);
        }

        [HttpPost]
        [Description("更新")]
        public async Task<IResultModel> Update(MenuUpdateModel model)
        {
            return await _service.Update(model);
        }

        [HttpGet]
        [Description("详情")]
        public Task<IResultModel> Details([BindRequired]Guid id)
        {
            return _service.Details(id);
        }

        [HttpGet]
        [Description("获取菜单的权限列表")]
        public Task<IResultModel> PermissionList([BindRequired]Guid id)
        {
            return _service.PermissionList(id);
        }

        [HttpPost]
        [Description("绑定权限")]
        public async Task<IResultModel> BindPermission(MenuBindPermissionModel model)
        {
            return await _service.BindPermission(model);
        }

        [HttpGet]
        [Description("获取菜单的按钮列表")]
        public Task<IResultModel> ButtonList([BindRequired]Guid id)
        {
            return _service.ButtonList(id);
        }
    }
}
