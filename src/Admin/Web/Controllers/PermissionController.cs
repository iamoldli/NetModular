using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.PermissionService;
using NetModular.Module.Admin.Domain.Permission.Models;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("权限接口")]
    public class PermissionController : Web.ModuleController
    {
        private readonly IPermissionService _service;

        public PermissionController(IPermissionService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]PermissionQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpGet]
        [Description("权限树")]
        public Task<IResultModel> Tree()
        {
            return _service.GetTree();
        }

        [HttpGet]
        [Description("根据编码查询")]
        public async Task<IResultModel> QueryByCodes([FromQuery]List<string> codes)
        {
            if (codes == null || !codes.Any())
                return ResultModel.Success();

            return await _service.QueryByCodes(codes);
        }
    }
}
