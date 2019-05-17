using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.CodeGenerator.Application.EnumService;
using Nm.Module.CodeGenerator.Application.EnumService.ViewModels;
using Nm.Module.CodeGenerator.Domain.Enum.Models;

namespace Nm.Module.CodeGenerator.Web.Controllers
{
    [Description("枚举管理")]
    public class EnumController : ModuleController
    {
        private readonly IEnumService _service;

        public EnumController(IEnumService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery] EnumQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(EnumAddModel model)
        {
            return _service.Add(model);
        }

        [HttpDelete]
        [Description("删除")]
        public async Task<IResultModel> Delete([BindRequired] Guid id)
        {
            return await _service.Delete(id);
        }

        [HttpGet]
        [Description("编辑")]
        public async Task<IResultModel> Edit([BindRequired] Guid id)
        {
            return await _service.Edit(id);
        }

        [HttpPost]
        [Description("修改")]
        public Task<IResultModel> Update(EnumUpdateModel model)
        {
            return _service.Update(model);
        }


        [HttpGet]
        [Description("下拉列表")]
        public Task<IResultModel> Select()
        {
            return _service.Select();
        }
    }
}
