using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Auth.Web.Attributes;
using NetModular.Lib.Module.AspNetCore.Attributes;
using NetModular.Module.Admin.Application.ToolService;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("通用工具")]
    [Common]
    [DisableAuditing]
    public class ToolController : Web.ModuleController
    {
        private readonly IToolService _service;

        public ToolController(IToolService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("枚举下拉列表接口")]
        public IResultModel EnumSelect([BindRequired]string moduleCode, [BindRequired]string enumName, string libName = "Domain")
        {
            return _service.GetEnumSelect(moduleCode, enumName, libName);
        }
    }
}
