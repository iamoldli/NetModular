using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Utils.Core.Options;
using Nm.Lib.Utils.Core.Result;
using Nm.Lib.Utils.Mvc.Extensions;
using Nm.Lib.Utils.Mvc.Helpers;
using Nm.Module.Common.Application.AttachmentService;
using Nm.Module.Common.Application.AttachmentService.ViewModels;
using Nm.Module.Common.Domain.Attachment.Models;

namespace Nm.Module.Common.Web.Controllers
{
    [Description("附件表管理")]
    public class AttachmentController : ModuleController
    {
        private readonly ILoginInfo _loginInfo;
        private readonly IAttachmentService _service;
        private readonly FileUploadHelper _fileUploadHelper;
        private readonly ModuleCommonOptions _moduleCommonOptions;

        public AttachmentController(IAttachmentService service, ILoginInfo loginInfo, FileUploadHelper fileUploadHelper, ModuleCommonOptions moduleCommonOptions)
        {
            _service = service;
            _loginInfo = loginInfo;
            _fileUploadHelper = fileUploadHelper;
            _moduleCommonOptions = moduleCommonOptions;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]AttachmentQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("上传")]
        public async Task<IResultModel> Upload([FromForm]AttachmentUploadModel model, IFormFile formFile)
        {
            model.AccountId = _loginInfo.AccountId;
            var uploadModel = new FileUploadModel
            {
                Request = Request,
                FormFile = formFile,
                RootPath = _moduleCommonOptions.UploadPath,
                Module = "Common",
                Group = Path.Combine("Attachment", model.Module, model.Group)
            };

            //附件存储路径/Common/Attachment/{所属模块名称}/{所属分组模块}
            var result = await _fileUploadHelper.Upload(uploadModel);

            if (result.Successful)
            {
                var resultModel = await _service.Upload(model, result.Data);
                if (resultModel.Successful)
                {
                    var url = Request.GetHost($"/common/attachment/download/{resultModel.Data.Id}");
                    resultModel.Data.Url = new Uri(url).ToString();
                    return ResultModel.Success(resultModel);
                }
            }

            return ResultModel.Failed("上传失败");
        }

        [HttpGet("{id:guid}")]
        [Description("下载")]
        public async Task<IActionResult> Download([BindRequired]Guid id)
        {
            var result = await _service.Download(id, _loginInfo.AccountId);
            if (result.Successful)
            {
                var file = result.Data;
                return PhysicalFile(file.FilePath, file.MediaType, file.Name, true);
            }

            return new JsonResult(result);
        }

        [HttpGet("{id:guid}")]
        [Description("导出")]
        public async Task<IActionResult> Export([BindRequired]Guid id)
        {
            var result = await _service.Download(id, _loginInfo.AccountId);
            if (result.Successful)
            {
                var file = result.Data;
                return PhysicalFile(file.FilePath, file.MediaType, file.Name, true);
            }

            return new JsonResult(result);
        }
    }
}
