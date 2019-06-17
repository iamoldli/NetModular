using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Nm.Lib.Auth.Abstractions.Attributes;
using Nm.Lib.Module.Abstractions.Attributes;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Utils.Core.Options;
using Nm.Lib.Utils.Core.Result;
using Nm.Lib.Utils.Mvc.Extensions;
using Nm.Lib.Utils.Mvc.Helpers;
using Nm.Module.Admin.Application.SystemService;
using Nm.Module.Admin.Application.SystemService.ViewModels;
using Nm.Module.Admin.Web.Core;

namespace Nm.Module.Admin.Web.Controllers
{
    [Description("系统")]
    public class SystemController : ModuleController
    {
        private readonly ModuleCommonOptions _options;
        private readonly ISystemService _systemService;
        private readonly FileUploadHelper _fileUploadHelper;
        private readonly PermissionHelper _permissionHelper;
        private readonly MvcHelper _mvcHelper;

        public SystemController(ISystemService systemService, IOptionsMonitor<ModuleCommonOptions> optionsMonitor, FileUploadHelper fileUploadHelper, PermissionHelper permissionHelper, MvcHelper mvcHelper)
        {
            _systemService = systemService;
            _fileUploadHelper = fileUploadHelper;
            _permissionHelper = permissionHelper;
            _mvcHelper = mvcHelper;
            _options = optionsMonitor.CurrentValue;
        }

        [HttpGet]
        [AllowAnonymous]
        [DisableAuditing]
        [Description("获取系统配置信息")]
        public Task<IResultModel<SystemConfigModel>> Config()
        {
            return _systemService.GetConfig(Request.GetHost());
        }

        [HttpPost]
        [Description("修改系统配置")]
        public IResultModel Config(SystemConfigModel model)
        {
            return _systemService.UpdateConfig(model);
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

        [HttpPost]
        [Description("系统初始化")]
        [AllowAnonymous]
        public Task<IResultModel> Install()
        {
            var model = new SystemInstallModel
            {
                Permissions = _permissionHelper.GetAllPermission()
            };
            return _systemService.Install(model);
        }

        [HttpGet]
        [Description("获取指定模块的Controller下拉列表")]
        public IResultModel AllController([BindRequired]string module)
        {
            var list = _mvcHelper.GetAllController().Where(m => m.Area.EqualsIgnoreCase(module)).Select(m => new OptionResultModel
            {
                Label = m.Description,
                Value = m.Name
            }).ToList();

            return ResultModel.Success(list);
        }

        [HttpGet]
        [Description("获取指定模块和Controller的Action下拉列表")]
        public IResultModel AllAction([BindRequired]string module, [BindRequired]string controller)
        {
            var list = _mvcHelper.GetAllAction().Where(m =>
                m.Controller.Area.EqualsIgnoreCase(module)
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
