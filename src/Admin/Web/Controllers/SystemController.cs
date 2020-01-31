using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using NetModular.Lib.Auth.Web.Attributes;
using NetModular.Lib.Module.AspNetCore.Attributes;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Options;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Lib.Utils.Core.SystemConfig;
using NetModular.Lib.Utils.Mvc.Extensions;
using NetModular.Lib.Utils.Mvc.Helpers;
using NetModular.Module.Admin.Application.SystemService;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("系统")]
    public class SystemController : ModuleController
    {
        private readonly ModuleCommonOptions _options;
        private readonly ISystemService _systemService;
        private readonly FileUploadHelper _fileUploadHelper;
        private readonly MvcHelper _mvcHelper;
        private readonly SystemConfigModel _configModel;

        public SystemController(ISystemService systemService, IOptionsMonitor<ModuleCommonOptions> optionsMonitor, FileUploadHelper fileUploadHelper, MvcHelper mvcHelper, SystemConfigModel configModel)
        {
            _systemService = systemService;
            _fileUploadHelper = fileUploadHelper;
            _mvcHelper = mvcHelper;
            _configModel = configModel;
            _options = optionsMonitor.CurrentValue;
        }

        [HttpGet]
        [AllowAnonymous]
        [DisableAuditing]
        [Description("获取系统配置信息")]
        public IResultModel<SystemConfigModel> Config()
        {
            var result = new ResultModel<SystemConfigModel>();

            var b = _configModel.Base;
            if (b.LogoUrl.IsNull() && b.Logo.NotNull())
            {
                b.LogoUrl = new Uri($"{Request.GetHost()}/upload/{b.Logo}").ToString().ToLower();
            }

            return result.Success(_configModel);
        }

        [HttpPost]
        [Description("修改系统基础配置")]
        public IResultModel UpdateBaseConfig(SystemBaseConfigModel model)
        {
            return _systemService.UpdateBaseConfig(model);
        }

        [HttpPost]
        [Description("修改系统组件配置")]
        public IResultModel UpdateComponentConfig(SystemComponentConfigModel model)
        {
            return _systemService.UpdateComponentConfig(model);
        }

        [HttpPost]
        [Description("修改系统登录配置")]
        public IResultModel UpdateLoginConfig(SystemLoginConfigModel model)
        {
            return _systemService.UpdateLoginConfig(model);
        }

        [HttpPost]
        [Description("修改系统权限配置")]
        public IResultModel UpdatePermissionConfig(SystemPermissionConfigModel model)
        {
            return _systemService.UpdatePermissionConfig(model);
        }

        [HttpPost]
        [Description("修改系统权限配置")]
        public IResultModel UpdateToolbarConfig(SystemToolbarConfigModel model)
        {
            return _systemService.UpdateToolbarConfig(model);
        }

        [HttpPost]
        [Description("上传Logo")]
        public async Task<IResultModel> UploadLogo(IFormFile formFile)
        {
            var model = new FileUploadModel
            {
                Request = Request,
                FormFile = formFile,
                RootPath = _options.UploadPath,
                Module = "Admin",
                Group = "Logo"
            };

            var result = await _fileUploadHelper.Upload(model);

            if (result.Successful)
            {
                var file = result.Data;

                file.Url = new Uri(Request.GetHost($"/upload/{file.FullPath.ToLower()}")).ToString().ToLower();

                return ResultModel.Success(file);
            }

            return ResultModel.Failed("上传失败");
        }

        [HttpGet]
        [Common]
        [Description("获取指定模块的Controller下拉列表")]
        public IResultModel AllController([BindRequired]string module)
        {
            var list = _mvcHelper.GetAllController().Where(m => m.Area.NotNull() && m.Area.EqualsIgnoreCase(module)).Select(m => new OptionResultModel
            {
                Label = m.Description,
                Value = m.Name
            }).ToList();

            return ResultModel.Success(list);
        }

        [HttpGet]
        [Common]
        [Description("获取指定模块和Controller的Action下拉列表")]
        public IResultModel AllAction([BindRequired]string module, [BindRequired]string controller)
        {
            var list = _mvcHelper.GetAllAction().Where(m =>
                m.Controller.Area.NotNull()
                && m.Controller.Area.EqualsIgnoreCase(module)
                && m.Controller.Name.EqualsIgnoreCase(controller)
                && !m.MethodInfo.CustomAttributes.Any(n => n.AttributeType == typeof(AllowAnonymousAttribute) || n.AttributeType == typeof(CommonAttribute)))
                .Select(m => new OptionResultModel
                {
                    Label = m.Description,
                    Value = m.Name
                }).ToList();

            return ResultModel.Success(list);
        }
    }
}
