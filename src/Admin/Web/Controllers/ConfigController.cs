using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Config.Abstractions.Impl;
using NetModular.Lib.Module.AspNetCore.Attributes;
using NetModular.Lib.Utils.Mvc.Extensions;
using NetModular.Lib.Utils.Mvc.Helpers;
using NetModular.Module.Admin.Application.ConfigService;
using NetModular.Module.Admin.Application.ConfigService.ViewModels;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("配置管理")]
    public class ConfigController : Web.ModuleController
    {
        private readonly IConfigService _service;
        private readonly FileUploadHelper _fileUploadHelper;
        private readonly IConfigProvider _configProvider;
        private readonly IConfigCollection _configCollection;
        public ConfigController(IConfigService service, FileUploadHelper fileUploadHelper, IConfigProvider configProvider, IConfigCollection configCollection)
        {
            _service = service;
            _fileUploadHelper = fileUploadHelper;
            _configProvider = configProvider;
            _configCollection = configCollection;
        }

        [HttpGet]
        [AllowAnonymous]
        [DisableAuditing]
        [Description("UI配置信息")]
        public IResultModel UI()
        {
            var result = _service.GetUI();
            if (result.System.Logo.NotNull())
            {
                result.System.Logo = new Uri($"{Request.GetHost()}/upload/{result.System.Logo}").ToString().ToLower();
            }

            return ResultModel.Success(result);
        }

        [HttpGet]
        [Description("编辑")]
        public IResultModel Edit(string code, ConfigType type)
        {
            return _service.Edit(code, type);
        }

        [HttpPost]
        [Description("保存")]
        public IResultModel Update(ConfigUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpPost]
        [Description("上传Logo")]
        public async Task<IResultModel> UploadLogo(IFormFile formFile)
        {
            var config = _configProvider.Get<PathConfig>();

            var model = new FileUploadModel
            {
                Request = Request,
                FormFile = formFile,
                RootPath = config.UploadPath,
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
        [Description("Logo完整地址")]
        public IResultModel LogoUrl()
        {
            var config = _configProvider.Get<SystemConfig>();
            var url = "";
            if (config.Logo.NotNull())
            {
                url = new Uri($"{Request.GetHost()}/upload/{config.Logo}").ToString().ToLower();
            }

            return ResultModel.Success(url);
        }

        [HttpGet]
        [Description("配置描述符列表")]
        public IResultModel Descriptors()
        {
            return ResultModel.Success(_configCollection);
        }
    }
}
