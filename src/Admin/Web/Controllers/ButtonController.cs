using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.ButtonService;
using NetModular.Module.Admin.Domain.Button.Models;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("按钮管理")]
    public class ButtonController : Web.ModuleController
    {
        private readonly IButtonService _service;

        public ButtonController(IButtonService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]ButtonQueryModel model)
        {
            return _service.Query(model);
        }
    }
}
