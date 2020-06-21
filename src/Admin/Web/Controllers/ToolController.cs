using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Abstractions.LoginModels;
using NetModular.Lib.Auth.Web.Attributes;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Utils.Mvc.Helpers;
using NetModular.Module.Admin.Application.ToolService;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("通用工具")]
    [Common]
    [DisableAuditing]
    public class ToolController : Web.ModuleController
    {
        private readonly IToolService _service;

        public ToolController(IToolService service, FileUploadHelper fileUploadHelper, IConfigProvider configProvider)
        {
            _service = service;
        }

        [HttpGet]
        [Description("通用枚举下拉列表")]
        public IResultModel EnumSelect([BindRequired] string moduleCode, [BindRequired] string enumName, string libName = "Domain")
        {
            return _service.GetEnumSelect(moduleCode, enumName, libName);
        }

        [HttpGet]
        [Description("平台类型下拉列表")]
        public IResultModel PlatformSelect()
        {
            return ResultModel.Success(EnumExtensions.ToResult<Platform>());
        }

        [HttpGet]
        [Description("登录模式下拉列表")]
        public IResultModel LoginModeSelect()
        {
            return ResultModel.Success(EnumExtensions.ToResult<LoginMode>());
        }
    }
}
