using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Application.ButtonService;
using Nm.Module.Admin.Domain.Button.Models;

namespace Nm.Module.Admin.Web.Controllers
{
    [Description("按钮管理")]
    public class ButtonController : ModuleController
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
