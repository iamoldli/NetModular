using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tm.Lib.Module.AspNetCore.Attributes;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.Admin.Application.AuditInfoService;
using Tm.Module.Admin.Domain.AuditInfo.Models;

namespace Tm.Module.Admin.Web.Controllers
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
