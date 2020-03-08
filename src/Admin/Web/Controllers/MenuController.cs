using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Utils.Core.Models;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.MenuService;
using NetModular.Module.Admin.Application.MenuService.ViewModels;
using NetModular.Module.Admin.Domain.Menu.Models;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("菜单管理")]
    public class MenuController : Web.ModuleController
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

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(MenuAddModel model)
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
        [Description("更新")]
        public Task<IResultModel> Update(MenuUpdateModel model)
        {
            return _service.Update(model);
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
