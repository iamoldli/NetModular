using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Common.Application.AttachmentService;
using Nm.Module.Common.Application.AttachmentService.ViewModels;
using Nm.Module.Common.Domain.Attachment.Models;

namespace Nm.Module.Common.Web.Controllers
{
    [Description("附件表管理")]
    public class AttachmentController : ModuleController
    {
        private readonly IAttachmentService _service;

        public AttachmentController(IAttachmentService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]AttachmentQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("上传")]
        public Task<IResultModel> Upload([FromForm]AttachmentUploadModel model)
        {
            return _service.Upload(model, Request);
        }

        [HttpGet("{id:guid}")]
        [Description("下载")]
        public async Task<IActionResult> Download([BindRequired]Guid id)
        {
            var result = await _service.Download(id);
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
            var result = await _service.Download(id);
            if (result.Successful)
            {
                var file = result.Data;
                return PhysicalFile(file.FilePath, file.MediaType, file.Name, true);
            }

            return new JsonResult(result);
        }
    }
}
