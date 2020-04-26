using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetModular.Module.Admin.Application.LogService;
using NetModular.Module.Admin.Domain.LoginLog.Models;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("日志管理")]
    public class LogController : Web.ModuleController
    {
        private readonly ILogService _service;

        public LogController(ILogService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询登录日志")]
        public Task<IResultModel> Login([FromQuery]LoginLogQueryModel model)
        {
            return _service.QueryLogin(model);
        }

        [HttpPost]
        [Description("导出登录日志")]
        public async Task<IActionResult> LoginExport(LoginLogQueryModel model)
        {
            var result = await _service.ExportLogin(model);
            if (result.Successful)
            {
                return ExportExcel(result.Data.Path, result.Data.FileName);
            }

            return Ok(result);
        }
    }
}
