using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Auth.Web.Attributes;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.ModuleInfoService;
using NetModular.Module.Admin.Domain.ModuleInfo.Models;

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
        public Task<IResultModel> Query([FromQuery]ModuleInfoQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("同步模块数据")]
        public Task<IResultModel> Sync()
        {
            return _service.Sync();
        }

        [HttpDelete]
        [Description("删除")]
        public Task<IResultModel> Delete([BindRequired]Guid id)
        {
            return _service.Delete(id);
        }

        [HttpGet]
        [Common]
        [Description("下拉列表数据")]
        public Task<IResultModel> Select()
        {
            return _service.Select();
        }
    }
}
