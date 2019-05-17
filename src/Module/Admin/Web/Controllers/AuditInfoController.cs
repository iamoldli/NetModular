using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nm.Lib.Module.Abstractions.Attributes;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Application.AuditInfoService;
using Nm.Module.Admin.Domain.AuditInfo.Models;

namespace Nm.Module.Admin.Web.Controllers
{
    [Description("审计信息")]
    public class AuditInfoController : ModuleController
    {
        private readonly IAuditInfoService _service;

        public AuditInfoController(IAuditInfoService service)
        {
            _service = service;
        }

        [HttpGet]
        [DisableAuditing]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]AuditInfoQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpGet]
        [DisableAuditing]
        [Description("详情")]
        public Task<IResultModel> Details([BindRequired]int id)
        {
            return _service.Details(id);
        }
    }
}
