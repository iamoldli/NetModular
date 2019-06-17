using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Common.Application.AttachmentService;
using Nm.Module.Common.Domain.Attachment.Models;
using Nm.Module.Common.Infrastructure.Options;

namespace Nm.Module.Common.Web.Controllers
{
    [Description("附件表管理")]
    public class AttachmentController : ModuleController
    {
        private readonly CommonOptions _options;
        private readonly IAttachmentService _service;

        public AttachmentController(IAttachmentService service, IOptionsMonitor<CommonOptions> optionsMonitor)
        {
            _service = service;
            _options = optionsMonitor.CurrentValue;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery] AttachmentQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("上传")]
        public Task<IResultModel> Upload(IFormFile formFile)
        {


            return _service.Add(null);
        }
    }
}
