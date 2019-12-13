using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Auth.Web.Attributes;
using NetModular.Lib.Module.AspNetCore.Attributes;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.AuditInfoService;
using NetModular.Module.Admin.Domain.AuditInfo.Models;

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

        [HttpGet]
        [DisableAuditing]
        [Common]
        public Task<IResultModel> QueryLatestWeekPv()
        {
            return _service.QueryLatestWeekPv();
        }

        [HttpPost]
        [Description("导出")]
        public async Task<IActionResult> Export(AuditInfoQueryModel model)
        {
            var result = await _service.Export(model);
            if (result.Successful)
            {
                return ExportExcel(result.Data.Path, result.Data.FileName);
            }

            return Ok(result);
        }
    }
}
