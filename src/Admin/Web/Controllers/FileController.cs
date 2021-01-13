using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Web.Attributes;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Config.Abstractions.Impl;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Utils.Core.Enums;
using NetModular.Lib.Utils.Mvc.Helpers;
using NetModular.Module.Admin.Application.FileService;
using NetModular.Module.Admin.Domain.File;
using NetModular.Module.Admin.Domain.File.Models;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("文件管理")]
    public class FileController : Web.ModuleController
    {
        private readonly FileUploadHelper _fileUploadHelper;
        private readonly IConfigProvider _configProvider;
        private readonly IFileService _service;
        private readonly IModuleCollection _moduleCollection;
        private readonly ILoginInfo _loginInfo;

        public FileController(FileUploadHelper fileUploadHelper, IConfigProvider configProvider, IFileService service, IModuleCollection moduleCollection, ILoginInfo loginInfo)
        {
            _fileUploadHelper = fileUploadHelper;
            _configProvider = configProvider;
            _service = service;
            _moduleCollection = moduleCollection;
            _loginInfo = loginInfo;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]FileQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("上传")]
        [Common]        
        //[RequestSizeLimit(1073741824)]
        [DisableRequestSizeLimit]
        public async Task<IResultModel> Upload([FromForm]Application.FileService.ViewModels.FileUploadModel model, IFormFile formFile)
        {
            var config = _configProvider.Get<PathConfig>();
            var module = _moduleCollection.FirstOrDefault(m => m.Code.EqualsIgnoreCase(model.ModuleCode));
            if (module == null)
                return ResultModel.Success("指定模块不存在");

            //使用模块编码，防止大小写问题
            model.ModuleCode = module.Code;

            var uploadModel = new FileUploadModel
            {
                Request = Request,
                FormFile = formFile,
                RootPath = config.UploadPath,
                Module = "Admin",
                Group = Path.Combine("OSS", model.AccessMode == FileAccessMode.Open ? "Open" : "Private"),
                SubPath = Path.Combine(model.ModuleCode.ToLower(), model.Group.ToLower())
            };

            var result = await _fileUploadHelper.Upload(uploadModel);
            if (!result.Successful)
                return result;

            //文件本地物理路径
            model.PhysicalPath = Path.Combine(config.UploadPath, result.Data.FullPath);

            //文件的OSS相对路径
            result.Data.Path = Path.Combine(result.Data.Path.Split(Path.DirectorySeparatorChar).Skip(3).ToArray());

            return await _service.Add(model, result.Data);
        }

        [HttpGet]
        [Description("根据ID获取")]
        public Task<IResultModel<FileEntity>> Get([BindRequired]int id)
        {
            return _service.Get(id);
        }

        [HttpGet("/api/admin/file/{**fullPath}")]
        [Description("根据完整路径获取")]
        [Common]
        public Task<IResultModel> Get([BindRequired]string fullPath)
        {
            return _service.Get(fullPath);
        }

        [HttpDelete]
        [Description("删除(逻辑删除)")]
        public Task<IResultModel> Delete([BindRequired]int id)
        {
            return _service.Delete(id);
        }

        [HttpDelete]
        [Description("删除(物理删除)")]
        public Task<IResultModel> HardDelete([BindRequired]int id)
        {
            return _service.HardDelete(id);
        }

        [HttpGet("/oss/p/{**fullPath}")]
        [Description("私有文件下载")]
        [Common]
        public async Task<IActionResult> Download([BindRequired]string fullPath)
        {
            fullPath = HttpUtility.UrlDecode(fullPath);
            var result = await _service.Download(fullPath, _loginInfo.AccountId);
            if (!result.Successful)
                return new JsonResult(result);

            var file = result.Data;
            var config = _configProvider.Get<PathConfig>();
            var path = Path.Combine(config.UploadPath, "Admin", "OSS", "Private", fullPath);
            return PhysicalFile(path, file.Mime, file.FileName, true);
        }
    }
}
