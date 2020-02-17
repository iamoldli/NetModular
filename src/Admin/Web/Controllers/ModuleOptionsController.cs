using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.ModuleOptionsService;
using NetModular.Module.Admin.Application.ModuleOptionsService.ViewModels;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("模块配置项管理")]
    public class ModuleOptionsController : ModuleController
    {
        private readonly IModuleOptionsService _service;

        public ModuleOptionsController(IModuleOptionsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("编辑模块配置信息")]
        public IResultModel Edit([BindRequired]string moduleCode)
        {
            return _service.Edit(moduleCode);
        }

        [HttpPost]
        [Description("更新模块配置信息")]
        public IResultModel Update(ModuleOptionsUpdateModel model)
        {
            return _service.Update(model);
        }
    }
}
