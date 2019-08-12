using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tm.Lib.Auth.Web.Attributes;
using Tm.Lib.Utils.Core.Models;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.CodeGenerator.Application.EnumItemService;
using Tm.Module.CodeGenerator.Application.EnumItemService.ViewModels;
using Tm.Module.CodeGenerator.Domain.EnumItem.Models;

namespace Tm.Module.CodeGenerator.Web.Controllers
{
    [Description("枚举项管理")]
    [Common]
    public class EnumItemController : ModuleController
    {
        private readonly IEnumItemService _service;

        public EnumItemController(IEnumItemService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]EnumItemQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(EnumItemAddModel model)
        {
            return _service.Add(model);
        }

        [HttpDelete]
        [Description("删除")]
        public async Task<IResultModel> Delete([BindRequired]Guid id)
        {
            return await _service.Delete(id);
        }

        [HttpGet]
        [Description("编辑")]
        public async Task<IResultModel> Edit([BindRequired]Guid id)
        {
            return await _service.Edit(id);
        }

        [HttpPost]
        [Description("修改")]
        public Task<IResultModel> Update(EnumItemUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpGet]
        [Description("获取排序信息")]
        public Task<IResultModel> Sort([BindRequired]Guid enumId)
        {
            return _service.QuerySortList(enumId);
        }

        [HttpPost]
        [Description("更新排序信息")]
        public Task<IResultModel> Sort(SortUpdateModel<Guid> model)
        {
            return _service.UpdateSortList(model);
        }
    }
}
