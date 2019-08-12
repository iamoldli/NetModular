using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tm.Lib.Auth.Web.Attributes;
using Tm.Lib.Module.AspNetCore.Attributes;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Lib.Utils.Core.Models;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.Admin.Application.MenuService;
using Tm.Module.Admin.Application.MenuService.ViewModels;
using Tm.Module.Admin.Domain.Menu;
using Tm.Module.Admin.Domain.Menu.Models;

namespace Tm.Module.Admin.Web.Controllers
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
        [Description("获取排序信息")]
        public Task<IResultModel> Sort(Guid? parentId)
        {
            return _service.QuerySortList(parentId ?? Guid.Empty);
        }

        [HttpPost]
        [Description("更新排序信息")]
        public Task<IResultModel> Sort(SortUpdateModel<Guid> model)
        {
            return _service.UpdateSortList(model);
        }
    }
}
