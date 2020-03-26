using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Auth.Web.Attributes;
using NetModular.Module.Admin.Application.ModuleService;
using NetModular.Module.Admin.Application.ModuleService.ViewModels;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("模块信息")]
    public class ModuleController : Web.ModuleController
    {
        private readonly IModuleService _service;

        public ModuleController(IModuleService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query()
        {
            return _service.Query();
        }

        [HttpGet]
        [Common]
        [Description("下拉列表")]
        public IResultModel Select()
        {
            return _service.Select();
        }

        [HttpGet]
        [Description("编辑模块配置信息")]
        public IResultModel OptionsEdit([BindRequired]string code)
        {
            return _service.OptionsEdit(code);
        }

        [HttpPost]
        [Description("更新模块配置信息")]
        public IResultModel OptionsUpdate(ModuleOptionsUpdateModel model)
        {
            return _service.OptionsUpdate(model);
        }
    }
}
