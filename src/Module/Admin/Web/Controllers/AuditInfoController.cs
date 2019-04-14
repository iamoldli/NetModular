using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Module.Abstractions.Attributes;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.AuditInfoService;
using NetModular.Module.Admin.Application.AuditInfoService.ViewModels;

namespace NetModular.Module.Admin.Web.Controllers
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
