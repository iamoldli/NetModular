using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.ModuleInfoService;
using NetModular.Module.Admin.Application.ModuleInfoService.ViewModels;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("模块信息")]
    public class ModuleInfoController : ModuleController
    {
        private readonly IModuleInfoService _service;

        public ModuleInfoController(IModuleInfoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]ModuleInfoQueryModel model)
        {
            return await _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(ModuleInfoAddModel model)
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
        public Task<IResultModel> Update(ModuleInfoUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpGet]
        [Description("下拉列表数据")]
        public Task<IResultModel> Select()
        {
            return _service.Select();
        }
    }
}
