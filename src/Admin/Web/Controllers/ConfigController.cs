using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Module.AspNetCore.Attributes;
using NetModular.Lib.OSS.Abstractions;
using NetModular.Module.Admin.Application.ConfigService;
using NetModular.Module.Admin.Application.ConfigService.ViewModels;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("配置管理")]
    public class ConfigController : Web.ModuleController
    {
        private readonly IConfigService _service;
        private readonly IConfigCollection _configCollection;
        private readonly IFileStorageProvider _fileStorageProvider;

        public ConfigController(IConfigService service, IConfigCollection configCollection, IFileStorageProvider fileStorageProvider)
        {
            _service = service;
            _configCollection = configCollection;
            _fileStorageProvider = fileStorageProvider;
        }

        [HttpGet]
        [AllowAnonymous]
        [DisableAuditing]
        [Description("UI配置信息")]
        public IResultModel UI()
        {
            var result = _service.GetUI();
            result.System.Logo = _fileStorageProvider.GetUrl(result.System.Logo);
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

        [HttpGet]
        [Description("配置描述符列表")]
        public IResultModel Descriptors()
        {
            return ResultModel.Success(_configCollection);
        }
    }
}
