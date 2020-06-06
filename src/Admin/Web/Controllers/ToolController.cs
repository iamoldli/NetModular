using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Web.Attributes;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Config.Abstractions.Impl;
using NetModular.Lib.Utils.Mvc.Extensions;
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
        public IResultModel EnumSelect([BindRequired]string moduleCode, [BindRequired]string enumName, string libName = "Domain")
        {
            return _service.GetEnumSelect(moduleCode, enumName, libName);
        }

        [HttpGet]
        [Description("平台下拉列表")]
        public IResultModel PlatformSelect()
        {
            return ResultModel.Success(EnumExtensions.ToResult<Platform>());
        }
    }
}
